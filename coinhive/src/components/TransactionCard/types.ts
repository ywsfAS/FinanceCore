export interface TransactionUI {
    id: number;
    name: string;
    account: string;
    date: string;
    amount: string;
    currency: string;
    category: string;
    positive: boolean;
}