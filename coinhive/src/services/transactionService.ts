import type { ImportedFileType } from '../components/TransactionActionPopUp/TransactionAction';
import { apiClient } from '../lib/apiClient';
export interface FiltredTransactionsParams {
    CategoryId?: string;
    Start?: Date | null;
    End?: Date | null;
    Type?: string;
    Page?: number;
    PageSize?: number;
}
export const TransactionType = {
    Income: "Income",
    Expense: "Expense",
    Transfer: "Transfer",
    Debt: "Debt",
    Credit: "Credit",
    CreditAdjustment: "CreditAdjustment",
    DebitAdjustment: "DebitAdjustment",
} as const;
export type TransactionType = typeof TransactionType[keyof typeof TransactionType];

export interface CreateTransactionParams {
    accountId: string;
    toAccountId?: string;
    categoryId: string;
    type: TransactionType;
    amount: number;
    description: string;
    transactionDate: string;
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
export interface ExportTransactionsParams {
    accountId?: string;
    toAccountId?: string;
    CategoryId?: string;
    Start?: Date | null;
    End?: Date | null;
    Type?: string;
    Page?: number;
    PageSize?: number;
}
export interface ImportTransactionsParams {
    type: ImportedFileType;
    file: File;
    accountId: string;
}
export interface GetTransactionByIdParams {
    id: string;
}

const transactionQuery = ({ accountId, toAccountId, CategoryId, Start, End, Type, Page, PageSize }: ExportTransactionsParams) => {
    const params = new URLSearchParams();
    if (accountId) params.set("accountId", accountId);
    if (toAccountId) params.set("toAccountId", toAccountId);
    if (CategoryId) params.set("CategoryId", CategoryId);
    if (Start) params.set("Start", Start.toISOString());
    if (End) params.set("End", End.toISOString());
    if (Type) params.set("Type", Type);
    if (Page) params.set("Page", String(Page));
    if (PageSize) params.set("PageSize", String(PageSize));
    return params.toString();
};
export const transactionService = {
    getFiltredTransactions: ({ CategoryId, Start, End, Type, Page = 1, PageSize = 5 }: FiltredTransactionsParams) => {
        const params: URLSearchParams = new URLSearchParams();
        if (CategoryId) params.append('CategoryId', CategoryId);
        if (Start) params.append('Start', Start.toISOString());
        if (End) params.append('End', End.toISOString());
        if (Type) params.append('Type', Type.toString())
        if (Page) params.append('Page', Page.toString());
        if (PageSize) params.append('PageSize', PageSize.toString());

        return apiClient(`/transactions?${params.toString()}`);
    },
    CreateTransaction: (transaction: CreateTransactionParams) => {
        return apiClient(`/transactions`, {
            method: 'POST',
            body: JSON.stringify(transaction)
        })
    },
    EditTransaction: ({ transactionId, transactionBody }: EditTransactionParams) => {

        return apiClient(`/transactions/${transactionId}`, {
            method: 'PUT',
            body: JSON.stringify(transactionBody),
        })
    },
    DeleteTransaction: ({ transactionId }: DeleteTransactionParams) => {

        return apiClient(`/transactions/${transactionId}`, {
            method: 'DELETE',
        })
    },
    ExportTransactions: (filters: ExportTransactionsParams) => apiClient<Blob>(`/transactions/export?${transactionQuery(filters)}`),
    ImportTransactions: ({ type, file, accountId }: ImportTransactionsParams) => {
        const formData = new FormData();
        formData.append("File", file);
        formData.append("AccountId", accountId);
        return apiClient(`/transactions/import/${type}`, { method: "POST", body: formData });
    },

    GetTransactionById: ({ id }: GetTransactionByIdParams) => {
        return apiClient(`/transactions/${id}`);
    }




}