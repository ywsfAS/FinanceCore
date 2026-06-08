import { apiClient } from '../lib/apiClient';
export interface FiltredTransactionsParams {
    CategoryId?: string;
    Start?: Date;
    End?: Date;
    Type?: number;
    Page?: number;
    PageSize?: number;
}
export const transactionService = {
    getFiltredTransactions: ({ CategoryId, Start, End, Type, Page = 1, PageSize = 5 }: FiltredTransactionsParams) => {
        const params : URLSearchParams = new URLSearchParams();
        if (CategoryId) params.append('CategoryId', CategoryId);
        if (Start) params.append('Start', Start.toISOString());
        if (End) params.append('End', End.toISOString());
        if(Type != undefined) params.append('Type',Type.toString())

        return apiClient(`/transactions?CategoryId=${params.toString()}`);
    }





}