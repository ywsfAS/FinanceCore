import { apiClient } from "../lib/apiClient";

export interface UserCategoryOption {
    id: string;
    name: string;
}

export interface UserAccountOption {
    id: string;
    name: string;
}


export const userService = {
    getUserCategoriesOptions: () => {
        return apiClient<UserCategoryOption[]>(`/categories/options?page=1&pageSize=10`);
    },
    getUserAccountsOptions: () => {
        return apiClient<UserAccountOption[]>(`/accounts/options?page=1&pageSize=100`);
    },
    getUserAccounts: () => {
        return apiClient(`/users/accounts`);
    },




}