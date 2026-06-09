import { apiClient } from "../lib/apiClient";



export const userService = {
    getUserCategoriesOptions : () => {
        return apiClient(`/users/categories/options`);
    }




}