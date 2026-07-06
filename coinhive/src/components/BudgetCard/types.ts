export type BudgetPeriod = "Weekly" | "Monthly" | "Yearly";

export interface BudgetCardProps {
    id: string;
    name: string;
    amount: number;
    currency: string;
    categoryName: string;
    period: BudgetPeriod;
    startDate: string;
    endDate: string;
    onEdit?: (id: string) => void;
    onDelete?: (id: string) => void | Promise<void>;
}
