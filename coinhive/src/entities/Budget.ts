export enum EnBudgetPeriod {
    Daily = "Daily",
    Weekly = "Weekly",
    Monthly = "Monthly",
    Quarterly = "Quarterly",
    Yearly = "Yearly",
    None = "None",
}

export interface BudgetEntity {
    id: string;
    name: string;
    amount: number;
    currency: string;
    period: EnBudgetPeriod | string;
    startDate: string;
    endDate: string;
    categoryName: string;
}
