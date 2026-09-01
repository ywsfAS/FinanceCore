export enum EnCategoryType {
    Income = "Income",
    Expense = "Expense",
    Both = "Both",
}

export interface CategoryEntity {
    id: string;
    userId?: string;
    name: string;
    type: EnCategoryType | string;
    description?: string;
}
