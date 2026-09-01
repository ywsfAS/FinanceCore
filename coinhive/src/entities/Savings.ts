export enum EnSavingsStatus {
    Active = "Active",
    Paused = "Paused",
    Completed = "Completed",
    Cancelled = "Cancelled",
}

export interface SavingsEntity {
    id: string;
    userId?: string;
    name: string;
    description?: string;
    targetAmount: number;
    currentAmount: number;
    currency: string;
    targetDate: string;
    status: EnSavingsStatus | string;
    completedAt?: string | null;
}
