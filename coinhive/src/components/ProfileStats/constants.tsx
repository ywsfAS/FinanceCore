import {
    ArrowUpNarrowWide,
    ArrowDownNarrowWide,
    PiggyBank,
    type LucideIcon
} from "lucide-react";

export type StatVariant = "income" | "expense" | "net";

export interface StatItem {
    icon: LucideIcon;
    label: string;
    value: string;
    change: string;
    variant: StatVariant;
}

export const stats: StatItem[] = [
    {
        icon: ArrowUpNarrowWide,
        label: "Total Income",
        value: "$1200.00",
        change: "↑ 12.4% vs last month",
        variant: "income"
    },
    {
        icon: ArrowDownNarrowWide,
        label: "Total Expenses",
        value: "$500.00",
        change: "↑ 4.1% vs last month",
        variant: "expense"
    },
    {
        icon: PiggyBank,
        label: "Net Balance",
        value: "$600,12",
        change: "↑ 18.9% vs last month",
        variant: "net"
    }
];