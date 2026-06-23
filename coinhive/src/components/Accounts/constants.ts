import type { GetAccountWithFiltersParams } from "../../services/accountService";

export enum AccountType {
    checking = "checking",
    credit = "credit",
    savings = "savings",
    cash = "cash",
}

export const INITIAL_FILTERS: GetAccountWithFiltersParams = {
    name: "",
    type: "",
    currency: "",
};

export const ACCOUNT_TYPES = [
    { value: "", label: "All Types" },
    { value: AccountType.checking, label: "Checking" },
    { value: AccountType.savings, label: "Savings" },
    { value: AccountType.cash, label: "Cash" },
    { value: AccountType.credit, label: "Credit" },
];

export const CURRENCIES = [
    { value: "", label: "All Currencies" },
    { value: "USD", label: "USD" },
    { value: "EUR", label: "EUR" },
    { value: "MAD", label: "MAD" },
];

export const MOCK_ACCOUNTS = [
    {
        id: 1,
        name: "Main Checking",
        type: AccountType.checking,
        balance: 4250,
        currency: "USD",
        label: "3.2% this month"
    },
    {
        id: 2,
        name: "Emergency Fund",
        type: AccountType.savings,
        balance: 12000,
        currency: "USD",
        label: "3.2% this month"
    },
    {
        id: 3,
        name: "Cash Wallet",
        type: AccountType.cash,
        balance: 350,
        currency: "MAD",
        label: "3.2% this month"
    },
    {
        id: 4,
        name: "Business Account",
        type: AccountType.checking,
        balance: 8450,
        currency: "EUR",
        label: "3.2% this month"
    },
];