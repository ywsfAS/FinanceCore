import { apiClient } from '../lib/apiClient';

export interface CreateAccountParams {
    name: string;
    type: string;
    currency: string;
    initialBalance: number;
}
export interface GetAccountByIdParams {
    id: string;
}
export interface GetAccountByNameParams {
    name: string;
}
export interface AccountDetails {
    id: string;
    userId: string;
    name: string;
    type: string;
    balance: number;
    currency: string;
    createdAt: string;
}
export interface RemoveAccountParams {
    id: string;
}
export interface GetAccountWithFiltersParams {
    name?: string;
    type?: string;
    currency?: string;
    page?: number;
    pageSize?: number;
}
export interface UpdateAccountParams {
    id: string;
    name: string;
    type: string;
}
export interface AccountAlertParams {
    accountId: string;
    thresholdAmount: number;
}
export interface ReconcileAccountParams {
    accountId: string;
    actualBalance: number;
    reason: string;
    notes?: string;
    createAdjustment: boolean;
}

export const accountService = {
    CreateAccount: (account: CreateAccountParams) => {
        return apiClient(`/accounts`, {
            method: 'POST',
            body: JSON.stringify(account),

        });
    },
    GetAccountById: ({ id }: GetAccountByIdParams) => {
        return apiClient<AccountDetails>(`/accounts/${id}`);
    },
    GetAccountByName: ({ name }: GetAccountByNameParams) => {
        return apiClient(`/accounts/${name}`);
    },
    RemoveAccount: ({ id }: RemoveAccountParams) => {
        return apiClient(`/accounts/${id}`, {
            method: 'DELETE'
        });
    },
    UpdateAccount: ({ id, name, type }: UpdateAccountParams) => {
        return apiClient(`/accounts/${id}`, {
            method: 'PUT',
            body: JSON.stringify({ name, type }),
        });
    },
    CreateAccountAlert: ({ accountId, thresholdAmount }: AccountAlertParams) => {
        return apiClient(`/accounts/${accountId}/alerts`, {
            method: 'POST',
            body: JSON.stringify({ thresholdAmount }),
        });
    },
    ReconcileAccount: ({ accountId, ...body }: ReconcileAccountParams) => {
        return apiClient(`/accounts/${accountId}/reconciliations`, {
            method: 'POST',
            body: JSON.stringify(body),
        });
    },
    getUserAccountsWithFilters: ({ currency, name, type, page, pageSize }: GetAccountWithFiltersParams) => {
        const params = new URLSearchParams();
        if (name) params.append('name', name);
        if (type) params.append('type', type);
        if (currency) params.append('currency', currency);
        if (page) params.append('page', String(page));
        if (pageSize) params.append('pageSize', String(pageSize));
        return apiClient(`/accounts?${params.toString()}`);
    }





}