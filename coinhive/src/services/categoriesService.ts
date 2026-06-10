import { apiClient } from "../lib/apiClient";


export interface CategoriesWithFiltersParams {
    name?: string;
    type?: string;
    page?: number;
    pageSize?: number;
};
export interface CreateCategoryParams {
    name: string;
    type: string;
    description?: string;
}
export interface UpdateCategoryParams {
    id: string;
    name: string;
    description: string;
}
export const categoriesService = {
    getCategoriesWithFilter: ({ name, type, page, pageSize }: CategoriesWithFiltersParams) => {
        const params = new URLSearchParams();
        if (name) params.append('name', name);
        if (type) params.append('type', type);
        if (page) params.append('page', page.toString());
        if (pageSize) params.append('pageSize',pageSize.toString());
        return apiClient(`/categories?${params}`);
    },
    CreateCategory : (cateogry : CreateCategoryParams) => {
        return apiClient(`/categories`, {
            method: 'POST',
            body: JSON.stringify(cateogry)
        });  
    },
    UpdateCategory: ({id , ...body} : UpdateCategoryParams) => {
        return apiClient(`/categories/${id}`, {
            method: 'PUT',
            body: JSON.stringify(body)
        });  
    }

}