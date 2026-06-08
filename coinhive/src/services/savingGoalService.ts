import { apiClient } from '../lib/apiClient';

export interface CreateGoalParams {
    name: string;
    targetAmount: number;
    currency: number;
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

export interface ReumeGoalParams {
    id: string;
}
export interface CancelGoalParams {
    id: string;
}

export const savingGoalService = {
    CreateGoal: (goal : CreateGoalParams) => {
        return apiClient(`/savings`, {
            method: 'POST',
            body : JSON.stringify(goal), 
        });
    },
    GetGoals: ({ page, pageSize }: GetGoalsParams) => {
        const params = new URLSearchParams();
        params.append('Page', page);
        params.append('PageSize', pageSize);
        return apiClient(`/savings${params}`)

    },
    RemoveGoal: ({ id }: RemoveGoalParams) => {
        return apiClient(`/savings/${id}`);
    },
    PauseGoal: ({id} : PauseGoalParams) => {
        return apiClient(`/savings/${id}/pause`);
    },
    ResumeGoal: ({id} : ResumeGoalParams) => {
        return apiClient(`/savings/${id}/resume`);
    },

    CancelGoal: ({id} : CancelGoalParams) => {
        return apiClient(`/savings/${id}/cancel`);
    },






}