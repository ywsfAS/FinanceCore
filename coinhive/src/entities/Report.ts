import { EnCurrency } from "./Currency";
export interface UserSummary {
    totalIncome: number;
    totalExpense: number;
    netSavings: number;
    currency: EnCurrency
}
export interface AccountSummary {
    accountId: string;
    totalIncome: number;
    totalExpense: number;
    netSavings: number;
    currency: EnCurrency

}
