import {
    ArrowUpNarrowWide,
    ArrowDownNarrowWide,
    PiggyBank,
    type LucideIcon
} from "lucide-react";

export interface StatItem {
    icon: LucideIcon;
    label: string;
}

export const stats: Record<string, StatItem> = {
    income: {
        icon: ArrowUpNarrowWide,
        label: "Total Income",
    },
    expense: {
        icon: ArrowDownNarrowWide,
        label: "Total Expenses",
    },
    savings: {
        icon: PiggyBank,
        label: "Net Balance",
    }
};