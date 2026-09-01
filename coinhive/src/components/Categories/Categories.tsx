import { useState } from 'react';

import styles from './Categories.module.css';

import CategoryCard from '../CategoryCard/CategoryCard';
import CategoryCreatePopup from '../CategoryCreatePopup/CategoryCreatePopup';
import type { CategoriesWithFiltersParams, RemoveCategoryParams } from '../../services/categoriesService';
import { useGetCategoriesWithFilters } from "../../hooks/Categories/useGetCategoriesWithFilter";
import { useRemoveCategory } from "../../hooks/Categories/useRemoveCategory";
import { initialCategoryFilter, HEADER } from "./constants";
import Input from "../Input/Input";
import CostumeSelect from "../Select/Select";
import { ACCOUNT_TYPES } from "../Accounts/constants";
import SectionHeader from '../SectionHeader/SectionHeader';
import type { CategoryEntity } from '../../entities/Category';

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
    };

    const handleClose = () => {
        setOpen((prev) => !prev);
    };

    const ChangeNameHandler = (name: string) => {
        setFilters((prev) => ({ ...prev, name }));
    };

    const ChangeTypeHandler = (type: string) => {
        setFilters((prev) => ({ ...prev, type }));
    };

    const { data, isLoading, error, isError } = useGetCategoriesWithFilters(filters);
    const categories: CategoryEntity[] = Array.isArray(data) ? data : [];

    return (
        <div className={styles.wrapper}>
            <SectionHeader title={HEADER.title} subtitle={HEADER.subtitle} btnName={HEADER.btnName} handler={() => setOpen(true)} />
            <div className={styles.filterSection}>
                <Input
                    placeholder="Search category..."
                    value={filters.name}
                    onChange={(e) => ChangeNameHandler(e.target.value)}
                />

                <CostumeSelect
                    value={filters.type}
                    onChange={(value) => ChangeTypeHandler(value)}
                    options={ACCOUNT_TYPES}
                />
            </div>

            <div className={styles.categoriesContainer}>
                {categories.length > 0 ? (
                    categories.map((cat) => (
                        <CategoryCard
                            name={cat.name}
                            type={cat.type}
                            id={cat.id}
                            icon={null}
                            key={cat.id}
                            onDelete={handleRemoveCategory}
                            amount={0}
                            percentage={0}
                        />
                    ))
                ) : (
                    <div>No categories available.</div>
                )}
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