import { Search, Plus } from 'lucide-react';
import { useState } from 'react';

import styles from './Categories.module.css';

import CategoryCard from '../CategoryCard/CategoryCard';
import CategoryCreatePopup from '../CategoryCreatePopup/CategoryCreatePopup';
import type {CategoriesWithFiltersParams , RemoveCategoryParams} from '../../services/categoriesService';
import { useGetCategoriesWithFilters } from "../../hooks/Categories/useGetCategoriesWithFilter";
import { useRemoveCategory } from "../../hooks/Categories/useRemoveCategory";
import { CATEGORY_CARDS, initialCategoryFilter } from "./constants";
import Button from "../Button/Button";
import Input from "../Input/Input";
import CostumeSelect from "../Select/Select";
import { ACCOUNT_TYPES  } from "../Accounts/constants";
const Categories = () => {
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



    return (
        <div className={styles.wrapper}>
            <div className={styles.header}>
                <div>
                    <h1 className={styles.title }>Categories</h1>
                    <p className={styles.subtitle }>
                        Manage your income and expense
                        categories
                    </p>
                </div>
                <Button
                    onClick={() => setOpen(true)}
                >
                    <Plus size={18} />
                    New Category
                </Button>
            </div>
            <div className={styles.filterSection}>
                    <Input
                        placeholder="Search category..."
                        value={filters.name}
                        onChange={(e) =>
                            ChangeNameHandler(e.target.value)
                        }
                    />

                <CostumeSelect
                    value={filters.type}
                    onChange={(value) =>
                        ChangeTypeHandler(value)
                    }
                    options={ACCOUNT_TYPES}
                   
                />
            </div>

            <div className={styles.categoriesContainer}>
                {CATEGORY_CARDS.map((cat) => <CategoryCard name={cat.name} type={cat.type} id={cat.id} icon={cat.icon} key={cat.id} onDelete={handleRemoveCategory} amount={cat.amount} percentage={cat.percentage} />)}
            </div>

            {open && (
                <CategoryCreatePopup
                    handleClose={handleClose}
                />
            )}
        </div>
    );
};

export default Categories;