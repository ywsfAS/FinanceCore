export interface DetailRow {
    key: string;
    value: string;
    pill?: {
        label: string;
        variant: "premium" | "moderate" | "verified";
    };
}

export interface Transaction {
    icon: string;
    name: string;
    date: string;
    amount: string;
    type: "credit" | "debit";
    tag: string;
    tagVariant: "food" | "salary" | "transport" | "shopping" | "subscript" | "transfer";
}