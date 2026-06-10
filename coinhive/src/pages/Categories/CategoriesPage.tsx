import { Search, Plus } from 'lucide-react';
import { useState } from 'react';

import styles from './CategoriesPage.module.css';

import CategoryCard from '../../components/CategoryCard/CategoryCard';
import CategoryCreatePopup from '../../components/CategoryCreatePopup/CategoryCreatePopup';
import type {CategoriesWithFiltersParams , RemoveCategoryParams} from '../../services/categoriesService';
import { useGetCategoriesWithFilters } from "../../hooks/Categories/useGetCategoriesWithFilter";
import { useRemoveCategory } from "../../hooks/Categories/useRemoveCategory";
const initialCategoryFilter: CategoriesWithFiltersParams = {
    name: "",
    type: "",
    page: 1,
    pageSize : 10,
}
const staticCategories = [
    { id: 1, name : "food" , type : "Expense" },
    { id: 2, name : "sport" , type : "Epense" },
    { id: 3, name : "gym" , type : "Expense" },

];
const CategoriesPage = () => {
    const [open, setOpen] = useState(false);
    const [filters, setFilters] = useState<CategoriesWithFiltersParams>(initialCategoryFilter);
    const RemoveCategoryMutation = useRemoveCategory();
    const handleRemoveCategory = async (id: string) => {
        const categoryId: RemoveCategoryParams = { id };
        try {
            await RemoveCategoryMutation.mutateAsync(categoryId);
            console.log("category deleted successfully");
        } catch (err) {
            console.error(err.message);
        }
    }

    const handleClose = () => {
        setOpen((prev) => !prev);
    }
    const ChangeNameHandler = (name : string) => {
        setFilters((prev) => ({ ...prev, name }));
    }
    const ChangeTypeHandler = (type: string) => {
        setFilters((prev) => ({ ...prev, type }));
    }
    
    const { data, isLoading, error, isError } = useGetCategoriesWithFilters(filters);
    if (isLoading) return <div>loading...</div>
    if (isError) return <div>{error.message}</div>

    const categories = data ?? staticCategories;


    return (
        <div className={styles.wrapper}>
            <div className={styles.header}>
                <div>
                    <h1>Categories</h1>
                    <p>
                        Manage your income and expense
                        categories
                    </p>
                </div>

                <button
                    className={styles.btn}
                    onClick={() => setOpen(true)}
                >
                    <Plus size={18} />
                    New Category
                </button>
            </div>

            <div className={styles.filterSection}>
                <div className={styles.searchContainer}>
                    <Search size={18} />

                    <input
                        placeholder="Search category..."
                        value={filters.name}
                        onChange={(e) =>
                            ChangeNameHandler(e.target.value)
                        }
                    />
                </div>

                <select
                    value={filters.type}
                    onChange={(e) =>
                        ChangeTypeHandler(e.target.value)
                    }
                >
                    <option value="">
                        All Types
                    </option>

                    <option value="Expense">
                        Expense
                    </option>

                    <option value="Income">
                        Income
                    </option>
                </select>
            </div>

            <div className={styles.categoriesContainer}>
                {categories.map((cat) => <CategoryCard name={cat.name} type={cat.type} id={cat.id} key={cat.id} onDelete={handleRemoveCategory} />)}
            </div>

            {open && (
                <CategoryCreatePopup
                    handleClose={() =>
                        setOpen(false)
                    }
                />
            )}
        </div>
    );
};

export default CategoriesPage;