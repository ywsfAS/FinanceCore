export enum EnTransactionType {
    Income = "Income",
    Expense = "Expense",
}

export interface TransactionEntity {
    id: string;
    accountName: string;
    toAccountName?: string | null;
    categoryName: string;
    amount: number;
    currency: string;
    type: EnTransactionType;
    date: string;
    description: string;
}
