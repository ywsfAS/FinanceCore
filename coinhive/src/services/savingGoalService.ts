import { apiClient } from '../lib/apiClient';

export interface CreateGoalParams {
    name: string;
    targetAmount: number;
    currency: string;
    targetDate: Date;
    description: string;
}
export interface GetGoalsParams {
    page: number;
    pageSize: number;
}
export interface RemoveGoalParams {
    id: string;
}
export interface PauseGoalParams {
    id: string;
}
export interface ResumeGoalParams {
    id: string;
}
export interface CancelGoalParams {
    id: string;
}

export interface SavingGoal {
    id: string;
    name: string;
    description: string;
    targetAmount: number;
    currentAmount: number;
    currency: string;
    targetDate: string;
    status: 'active' | 'paused' | 'completed' | 'cancelled';
}

export const savingGoalService = {
    CreateGoal: (goal: CreateGoalParams) => {
        return apiClient(`/savings`, {
            method: 'POST',
            body: JSON.stringify(goal),
        });
    },
    GetGoals: ({ page, pageSize }: GetGoalsParams) => {
        const params = new URLSearchParams();
        params.append('page', page.toString());
        params.append('pageSize', pageSize.toString());
        return apiClient(`/savings?${params.toString()}`);
    },
    RemoveGoal: ({ id }: RemoveGoalParams) => {
        return apiClient(`/savings/${id}`, {
            method: 'DELETE',
        });
    },
    PauseGoal: ({ id }: PauseGoalParams) => {
        return apiClient(`/savings/${id}/pause`, {
            method: 'POST',
        });
    },
    ResumeGoal: ({ id }: ResumeGoalParams) => {
        return apiClient(`/savings/${id}/resume`, {
            method: 'POST',
        });
    },
    CancelGoal: ({ id }: CancelGoalParams) => {
        return apiClient(`/savings/${id}/cancel`, {
            method: 'POST',
        });
    },
};


