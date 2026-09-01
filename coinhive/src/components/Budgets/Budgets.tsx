import { useState } from 'react';
import BudgetCard from '../BudgetCard/BudgetCard';
import BudgetCreatePopup from '../BudgetCreatePopup/BudgetCreatePopup';
import SectionHeader from '../SectionHeader/SectionHeader';
import styles from './Budgets.module.css';
import { HEADER } from './constants';
import { useGetBudgetsWithFilters } from '../../hooks/Budget/useBudgetsWithFilters';
import type { GetBudgetsWithFiltersParams } from '../../services/budgetService';
import type { BudgetEntity } from '../../entities/Budget';

const initialFilters: GetBudgetsWithFiltersParams = {
    name: '',
    categoryId: '',
    period: '',
};

const Budgets = () => {
    const [open, setOpen] = useState(false);
    const [filters] = useState<GetBudgetsWithFiltersParams>(initialFilters);
    const { data, isLoading } = useGetBudgetsWithFilters(filters);
    const budgets: BudgetEntity[] = Array.isArray(data) ? data : [];

    const handleClose = () => {
        setOpen((prev) => !prev);
    };

    if (isLoading) return <div>Loading...</div>;

    return (
        <div className={styles.wrapper}>
            <SectionHeader title={HEADER.title} subtitle={HEADER.subtitle} btnName={HEADER.btnName} handler={handleClose} />

            <div className={styles.cards}>
                {budgets.length > 0 ? (
                    budgets.map((budget) => (
                        <BudgetCard
                            key={budget.id}
                            id={budget.id}
                            name={budget.name}
                            amount={budget.amount}
                            currency={budget.currency}
                            categoryName={budget.categoryName}
                            period={budget.period as any}
                            startDate={budget.startDate}
                            endDate={budget.endDate}
                        />
                    ))
                ) : (
                    <div>No budgets available.</div>
                )}
            </div>

            {open && <BudgetCreatePopup handleClose={handleClose} />}
        </div>
    );
};

export default Budgets;