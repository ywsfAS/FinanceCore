import type { ReactNode } from "react";

export type CategoryEntry = {
    name: string;
    value: number;
};

export type MonthlyEntry = {
    month: string;
    amount: number;
};

export type CustomTooltipProps = {
    active?: boolean;
    payload?: Array<{ name?: string; value?: number; payload?: unknown }>;
};

export type CustomBarTooltipProps = {
    active?: boolean;
    payload?: Array<{ value: number }>;
    label?: string;
};

export type SummaryItem = {
    label: string;
    value: string | number;
};

export type LegendItem = {
    name: string;
    value: number;
    icon?: ReactNode;
};