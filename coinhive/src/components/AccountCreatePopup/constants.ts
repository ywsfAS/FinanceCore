import type { CreateAccountParams } from "../../services/accountService";
import { ACCOUNT_TYPES, CURRENCIES } from "../Accounts/constants";

export const CREATE_ACCOUNT_COPY = {
    title: "Create a New Account",
    description: "Add an account to track your balances and manage your finances.",
    fields: {
        name: {
            label: "Account Name",
            description: "Give this account a recognizable name.",
            placeholder: "e.g. Main Checking Account",
        },
        type: {
            label: "Account Type",
            description: "Select the type of account you want to create.",
            placeholder: "Select account type",
        },
        currency: {
            label: "Currency",
            description: "Choose the currency used by this account.",
            placeholder: "Select currency",
        },
        initialBalance: {
            label: "Initial Balance",
            description: "Enter the current balance for this account.",
            placeholder: "0.00",
        },
    },
    submit: "Create Account",
};

export const INITIAL_CREATE_ACCOUNT: CreateAccountParams = {
    name: "",
    currency: "USD",
    initialBalance: 0,
    type: "Cash",
};

export const CREATE_ACCOUNT_TYPES = ACCOUNT_TYPES.filter((option) => option.value).map((option) => option);
export const CREATE_ACCOUNT_CURRENCIES = CURRENCIES.map((option, index) => index === 0 ? { ...option, label: "Select Currency" } : option);