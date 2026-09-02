import {
    BanknoteArrowUp,
    ArrowLeftRight,
    CircleDollarSign,
    CreditCard,
    Landmark,
    Receipt,
} from "lucide-react";

export const TABLE_HEADERS = [
    "Icon",
    "Transaction/Category",
    "Account",
    "Date & Time",
    "Amount",
    "Status",
];

export const TRANSACTION_TYPE_ICONS = {
    Income: BanknoteArrowUp,
    Expense: Receipt,
    Transfer: ArrowLeftRight,
    Debt: Landmark,
    Credit: CreditCard,
    CreditAdjustment: CircleDollarSign,
    DebitAdjustment: CircleDollarSign,
} as const;
