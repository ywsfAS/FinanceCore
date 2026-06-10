import { apiClient } from '../lib/apiClient';

export interface CreateAccountParams {
    name: string;
    type: string;
    currency: number;
    initialBalance: number;
}
export interface GetAccountByIdParams {
    id: string;
}
export interface GetAccountByNameParams {
    name: string;
}
export interface RemoveAccountParams {
    id: string;
}
export interface GetAccountWithFiltersParams {
    name?: string;
    type?: string;
    currency: string;
}

export const accountService = {
    CreateAccount : (account : CreateAccountParams) => {
        return apiClient(`/accounts`, {
            method: 'POST',
            body : JSON.stringify(account),

        });
    },
    GetAccountById : ({id } : GetAccountByIdParams) => {
        return apiClient(`/accounts/${id}`);
    },
    GetAccountByName : ({ name }: GetAccountByNameParams) => {
        return apiClient(`/accounts/${name}`);
    },
    RemoveAccount : ({ id } : RemoveAccountParams) => {
        return apiClient(`/accounts/${id}`, {
            method: 'DELETE'
        });
    },
    getUserAccountsWithFilters: ({ currency, name, type }: GetAccountWithFiltersParams) => {
        const params = new URLSearchParams();
        if (name) params.append('name', name);
        if (type) params.append('type', type);
        if (currency) params.append('currency', currency);
        return apiClient(`/accounts?${params}`);
    }





}