import { apiClient } from "../lib/apiClient";



export const userService = {
    getUserCategoriesOptions : () => {
        return apiClient(`/users/categories/options`);
    },
    getUserAccountsOptions: () => {
        return apiClient(`/users/accounts/options`);
    },
    getUserAccounts: () => {
        return apiClient(`/users/accounts`);
    },




}