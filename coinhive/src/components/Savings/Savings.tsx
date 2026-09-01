import { useState } from "react";
import SectionHeader from "../SectionHeader/SectionHeader";
import Input from "../Input/Input";
import CustomSelect from "../Select/Select";
import SavingGoalCard from "./SavingGoalCard";
import styles from "./Savings.module.css";
import {
    CURRENCIES,
    HEADER,
    INITIAL_FILTERS,
    SAVING_STATUSES,
} from "./constants";
import type { SavingGoalsFilters } from "./types";
import { useGoals } from "../../hooks/Goals/useGoals";
import type { SavingsEntity } from "../../entities/Savings";

const Savings = () => {
    const [open, setOpen] = useState(false);
    const [filters, setFilters] = useState<SavingGoalsFilters>(INITIAL_FILTERS);
    const { data, isLoading } = useGoals({
        page: 1,
        pageSize: 10,
        currency: filters.currency || undefined,
        status: filters.status || undefined,
        name: filters.search || undefined,
    });
    const goals: SavingsEntity[] = Array.isArray(data) ? data : [];

    const handleToggle = () => setOpen((prev) => !prev);

    const updateFilter = (key: keyof SavingGoalsFilters, value: string) => {
        setFilters((prev) => ({
            ...prev,
            [key]: value,
        }));
    };

    return (
        <div>
            <SectionHeader
                title={HEADER.title}
                subtitle={HEADER.subtitle}
                btnName={HEADER.btnName}
                handler={handleToggle}
            />

            <div className={styles.filterBlock}>
                <Input
                    placeholder="Search goal..."
                    value={filters.search}
                    onChange={(e) => updateFilter("search", e.target.value)}
                />
                <div className={styles.filterRow}>
                    <CustomSelect
                        value={filters.currency}
                        onChange={(value) => updateFilter("currency", value)}
                        options={CURRENCIES}
                        variant="secondary"
                    />
                    <CustomSelect
                        value={filters.status}
                        onChange={(value) => updateFilter("status", value)}
                        options={SAVING_STATUSES}
                        variant="secondary"
                    />
                </div>
            </div>

            <div className={styles.goalsGrid}>
                {isLoading ? (
                    <div>Loading...</div>
                ) : goals.length ? (
                    goals.map((goal) => <SavingGoalCard key={goal.id} goal={{
                        id: goal.id,
                        name: goal.name,
                        description: goal.description ?? "",
                        targetAmount: goal.targetAmount,
                        currentAmount: goal.currentAmount,
                        currency: goal.currency,
                        targetDate: goal.targetDate,
                        status: (goal.status as any) ?? "active",
                    }} />)
                ) : (
                    <div className={styles.emptyState}>No saving goals available.</div>
                )}
            </div>

            {open && (
                <div className={styles.noticeCard}>
                    <strong>New goal creation</strong>
                    <p>Goal creation is not implemented yet, but the structure is ready for it.</p>
                </div>
            )}
        </div>
    );
};

export default Savings;
