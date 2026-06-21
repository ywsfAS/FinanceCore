import {
    BanknoteArrowDown,
    BanknoteArrowUp,
} from "lucide-react";
import type { TransactionUI } from "./types";

export const DEFAULT_TRANSACTIONS: TransactionUI[] = [
    {
        id: 1,
        name: "Zara Store - Shopping",
        account: "Mononiex",
        date: "Feb 20, 2026 14:25",
        amount: "$120.00",
        currency: "USD",
        category: "Shopping",
        positive: false,
    },
    {
        id: 2,
        name: "Salary Deposit",
        account: "Mononiex",
        date: "Feb 19, 2026 09:00",
        amount: "$2,500.00",
        currency: "USD",
        category: "Income",
        positive: true,
    },
    {
        id: 3,
        name: "Netflix Subscription",
        account: "Mononiex",
        date: "Feb 18, 2026 22:15",
        amount: "$15.99",
        currency: "USD",
        category: "Entertainment",
        positive: false,
    },
    {
        id: 4,
        name: "Freelance Payment",
        account: "PayPal",
        date: "Feb 17, 2026 11:40",
        amount: "$850.00",
        currency: "USD",
        category: "Income",
        positive: true,
    },
    {
        id: 5,
        name: "Amazon Purchase",
        account: "Mononiex",
        date: "Feb 16, 2026 16:30",
        amount: "$79.99",
        currency: "USD",
        category: "Shopping",
        positive: false,
    },
];
export const TABLE_HEADERS = [
    "Icon",
    "Transaction/Category",
    "Account",
    "Date & Time",
    "Amount",
    "Status",
];

export const getTransactionIcon = (positive: boolean) => {
    return positive
        ? <BanknoteArrowUp size={ 18 } />
        : <BanknoteArrowDown size={ 18 } />;
};