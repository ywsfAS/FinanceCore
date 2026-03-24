export interface CategoryEntry {
    name: string;
    value: number;
}

export interface MonthlyEntry {
    month: string;
    amount: number;
}

export interface CustomTooltipProps {
    active?: boolean;
    payload?: Array<{
        name?: string;
        value?: number;
        payload?: CategoryEntry;
    }>;
}

export interface CustomBarTooltipProps {
    active?: boolean;
    payload?: Array<{ value: number }>;
    label?: string;
}

export interface CustomBarProps {
    x?: number;
    y?: number;
    width?: number;
    height?: number;
    fill?: string;
}

export interface SummaryItem {
    label: string;
    value: string;
}
