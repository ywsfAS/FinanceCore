import type { LucideIcon } from "lucide-react";

export interface AnalyticsHeader {
    title: string;
    subtitle: string;
}

export interface AnalyticsStat {
    id: string;
    icon: LucideIcon;
    title: string;
    subtitle: string;
}

export interface AnalyticsMetric {
    icon: LucideIcon;
    title: string;
    subtitle: string;
    maxValue: number;
    value: number;
    label: string;
}
