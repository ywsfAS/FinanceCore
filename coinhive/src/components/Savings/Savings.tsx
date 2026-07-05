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
    MOCK_GOALS,
    SAVING_STATUSES,
} from "./constants";
import type { SavingGoalsFilters } from "./types";

const Savings = () => {
    const [open, setOpen] = useState(false);
    const [filters, setFilters] = useState<SavingGoalsFilters>(INITIAL_FILTERS);

    const handleToggle = () => setOpen((prev) => !prev);

    const updateFilter = (key: keyof SavingGoalsFilters, value: string) => {
        setFilters((prev) => ({
            ...prev,
            [key]: value,
        }));
    };

    const filteredGoals = MOCK_GOALS.filter((goal) => {
        const search = filters.search.trim().toLowerCase();
        const matchesSearch =
            !search ||
            goal.name.toLowerCase().includes(search) ||
            goal.description.toLowerCase().includes(search);
        const matchesCurrency = !filters.currency || goal.currency === filters.currency;
        const matchesStatus = !filters.status || goal.status === filters.status;

        return matchesSearch && matchesCurrency && matchesStatus;
    });

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
                {filteredGoals.length ? (
                    filteredGoals.map((goal) => <SavingGoalCard key={goal.id} goal={goal} />)
                ) : (
                    <div className={styles.emptyState}>No saving goals match the selected filters.</div>
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
