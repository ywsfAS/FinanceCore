import { Search, Plus } from 'lucide-react';
import { useState } from 'react';
import { useGetBudgetsWithFilters } from "../../hooks/Budget/useBudgetsWithFilters";
import type {GetBudgetsWithFiltersParams , RemoveBudgetParams} from "../../services/budgetService";
import { useUserCategoriesOptions } from "../../hooks/User/useUserCategoriesOptions";
import {useRemoveBudget} from "../../hooks/Budget/useRemoveBudget";
import styles from './BudgetsPage.module.css';
import BudgetCard from "../../components/BudgetCard/BudgetCard";
export enum BudgetPeriod {
    Daily = "Daily",
    Weekly = "Weekly",
    Monthly = "Monthly",
    Quarterly = "Quarterly",
    Yearly = "Yearly",
}
const initialFilters: GetBudgetsWithFiltersParams  = {
    name: "",
    categoryId: "",
    period: "",
}
const BudgetsPage = () => {
    const [filters, setFilters] = useState<GetBudgetsWithFiltersParams>(initialFilters);
    const { data : budgets, isLoading : budgetIsLoading, isError : budgetIsError, error : budgetError } = useGetBudgetsWithFilters(filters);
    const { data : categories, isLoading : categoriesIsLoading, isError : categoriesIsError, error  : categoryError} = useUserCategoriesOptions();
    const removeBudgetMutation = useRemoveBudget();
    const handleRemoveBudget = async (id: string) => {
        const budget: RemoveBudgetParams = { id };
        try {
            await removeBudgetMutation.mutateAsync(budget);
        }catch(err){
            console.error(err);
        }
    }
    const handleName = (name : string) => {
        setFilters((prev) => ({ ...prev, name }));
    }
    const handlePeriod = (period: string) => {
        const p = period as BudgetPeriod
        setFilters((prev) => ({ ...prev, period : p }));
    }
    const handleCategory = (categoryId : string) => {
        setFilters((prev) => ({ ...prev, categoryId }));
    }


    if (budgetIsLoading || categoriesIsLoading) return <div>Loading...</div>;
    if (budgetIsError || categoriesIsError) return <div>{budgetError + ' ' + categoriesIsError}</div>;

    return (
        <div className={styles.wrapper}>
            <div className={styles.header}>
                <div>
                    <h1>Budgets</h1>

                    <p>
                        Manage your spending limits
                        and financial planning
                    </p>
                </div>

                <button className={styles.btn}>
                    <Plus size={18} />
                    New Budget
                </button>
            </div>

            <div className={styles.filterSection}>
                <div className={styles.searchContainer}>
                    <Search size={18} />

                    <input
                        placeholder="Search budget..."
                        value={filters.name}
                        onChange={(e) =>
                            handleName(e.target.value)
                        }
                    />
                </div>

                <select
                    value={filters.categoryId}
                    onChange={(e) =>
                        handleCategory(e.target.value)
                    }
                >
                    <option value="">
                        All Categories
                    </option>
                    {categories.map((cat) => <option value={cat.id} key={cat.id} id={cat.id}>{cat.name}</option>) }
                </select>

                <select
                    value={filters.period}
                    onChange={(e) =>
                        handlePeriod(e.target.value)
                    }
                >
                    <option value="">
                        All Periods
                    </option>
                    {Object.values(BudgetPeriod).map((p) => <option value={p}>{p}</option>) }
                </select>
            </div>
            <div className={styles.budgetsContainer}>
                {budgets.map((b) => <BudgetCard id={b.id} name={b.name} amount={b.amount} currency={b.currency} categoryName={b.categoryName} period={b.period} startDate={b.startDate} endDate={b.endSate} onDelete={handleRemoveBudget} />) }
            </div>
        </div>
    );
};

export default BudgetsPage;