import type { GetAccountWithFiltersParams } from "../../services/accountService";
import { EnAccountType } from "../../entities/Account";

export enum AccountType {
    checking = EnAccountType.Checking,
    credit = EnAccountType.Credit,
    savings = EnAccountType.Savings,
    cash = EnAccountType.Cash,
}

export const INITIAL_FILTERS: GetAccountWithFiltersParams = {
    name: "",
    type: "",
    currency: "",
};

export const ACCOUNT_TYPES = [
    { value: "", label: "All Types" },
    { value: EnAccountType.Checking, label: "Checking" },
    { value: EnAccountType.Savings, label: "Savings" },
    { value: EnAccountType.Cash, label: "Cash" },
    { value: EnAccountType.Credit, label: "Credit" },
    { value: EnAccountType.Investment, label: "Investment" },
    { value: EnAccountType.Loan, label: "Loan" },
    { value: EnAccountType.Other, label: "Other" },
];

export const CURRENCIES = [
    { value: "", label: "All Currencies" },
    { value: "USD", label: "USD" },
    { value: "EUR", label: "EUR" },
    { value: "GBP", label: "GBP" },
    { value: "JPY", label: "JPY" },
    { value: "CAD", label: "CAD" },
    { value: "AUD", label: "AUD" },
    { value: "CHF", label: "CHF" },
    { value: "CNY", label: "CNY" },
    { value: "MXN", label: "MXN" },
    { value: "INR", label: "INR" },
];

export const HEADER = {
    title: "Accounts",
    subtitle: "Manage and monitor your financial accounts",
    btnName: "new Account",
};