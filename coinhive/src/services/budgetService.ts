import {apiClient} from "../lib/apiClient";

export interface GetBudgetByIdParams{
    id : string;
}
export interface GetBudgetsWithFiltersParams{
    name: string | "";
    categoryId : string | "";
    period : string | "";
    page? : number;
    pageSize? : number;

}
export interface CreateBudgetParams {
    categoryId : string;
    name : string;
    amount : number;
    currency : string;
    period : string;


}
export interface UpdateBudgetParams {
    id: string;
    name: string;
    amount: number;
    currency: string;
    period: string;
}
export interface RemoveBudgetParams {
    id: string;
}
export const budgetService =  {
    getBudgetById : ({id} : GetBudgetByIdParams) => {
        return apiClient(`/budgets/${id}`);
    },
    getBudgetsWithFilters : ({name , categoryId , period , page = 1 , pageSize = 10} : GetBudgetsWithFiltersParams) => {
        const params = new URLSearchParams();
        if (name) params.append('name', name);
        if(categoryId) params.append('categoryId',categoryId);
        if(period) params.append('period',period);
        if(page) params.append('page',page.toString());
        if(pageSize) params.append('pageSize',pageSize.toString());
        return apiClient(`/budgets?${params}`);
    },
    CreateBudget : (budget : CreateBudgetParams) => {
        return apiClient(`/budgets`,{
            method : 'POST',
            body : JSON.stringify(budget)
        });
    },
    EditBudget: ({id , ...body} : UpdateBudgetParams) => {
        return apiClient(`/budgets/${id}`, {
            method: 'PUT',
            body : JSON.stringify(body)
        })
    },
    RemoveBudget: ({id } : RemoveBudgetParams) => {
        return apiClient(`/budgets/${id}`, {
            method: 'DELETE'
        })
    }
}