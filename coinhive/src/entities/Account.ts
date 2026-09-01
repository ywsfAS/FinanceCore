import type { EnCurrency } from "./Currency";

export enum EnAccountType {
    Checking = "Checking",
    Savings = "Savings",
    Credit = "Credit",
    Investment = "Investment",
    Cash = "Cash",
    Loan = "Loan",
    Other = "Other",
}

export interface AccountEntity {
    id: string;
    name: string;
    type: EnAccountType;
    balance: number;
    currency: EnCurrency | string;
}
