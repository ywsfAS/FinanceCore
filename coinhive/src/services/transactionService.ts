import { apiClient } from '../lib/apiClient';
export interface FiltredTransactionsParams {
    CategoryId?: string;
    Start?: Date | null;
    End?: Date | null;
    Type?: number;
    Page?: number;
    PageSize?: number;
}
export interface CreateTransactionParams {
    accountId: string;
    categoryId: string;
    type: string;
    amount: number;
    description: string;
    transactionDate: Date;
}
export interface EditTransactionBodyParams {
    accountId: string;
    categoryId: string;
    type: string;
    amount: number;
    description: string;
    transactionDate: Date;
}
export interface EditTransactionParams {
    transactionId: string;
    transactionBody: EditTransactionBodyParams;

}
export interface DeleteTransactionParams {
    transactionId: string;
}
export const transactionService = {
    getFiltredTransactions: ({ CategoryId, Start, End, Type, Page = 1, PageSize = 5 }: FiltredTransactionsParams) => {
        const params : URLSearchParams = new URLSearchParams();
        if (CategoryId) params.append('CategoryId', CategoryId);
        if (Start) params.append('Start', Start.toISOString());
        if (End) params.append('End', End.toISOString());
        if (Type) params.append('Type', Type.toString())
        if (Page) params.append('Page', Page.toString());
        if (PageSize) params.append('PageSize', PageSize.toString());

        return apiClient(`/transactions?${params.toString()}`);
    },
    CreateTransaction : (transaction: CreateTransactionParams) => {
        return apiClient(`/transactions`, {
            method: 'POST',
            body: JSON.stringify(transaction)
        })
    },
    EditTransaction: ({transactionId ,transactionBody } : EditTransactionParams) => {

        return apiClient(`/transactions/${transactionId}`, {
            method: 'PUT',
            body: JSON.stringify(transactionBody),
        })
    },
    DeleteTransaction: ({transactionId}: DeleteTransactionParams) => {

        return apiClient(`/transactions/${transactionId}`, {
            method: 'DELETE',
        })
    }





}