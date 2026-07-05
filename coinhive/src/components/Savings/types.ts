export type SavingGoalStatus = 'active' | 'paused' | 'completed' | 'cancelled';

export interface SavingGoal {
    id: string;
    name: string;
    description: string;
    targetAmount: number;
    currentAmount: number;
    currency: string;
    targetDate: string;
    status: SavingGoalStatus;
}

export interface SavingGoalsFilters {
    search: string;
    currency: string;
    status: SavingGoalStatus | "";
}
