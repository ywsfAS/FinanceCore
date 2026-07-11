import { Activity } from 'lucide-react';
import { DollarSign } from 'lucide-react';
import { TrendingUp } from 'lucide-react';
import { Bell } from 'lucide-react';
import { BarChart3 } from 'lucide-react';
import { type AnalyticsStat, type AnalyticsMetric } from './types';

export const HEADER = {
    title: "Analytics",
    subtitle: "Monitor cashflow, category usage, and growth metrics with integrated charts and progress insights.",
    btnName: "Refresh",
};

export const SUMMARY_CARDS: AnalyticsStat[] = [
    {
        id: "summary-1",
        icon: TrendingUp,
        title: "Revenue trend",
        subtitle: "Monthly income is up 12% compared to last quarter.",
    },
    {
        id: "summary-2",
        icon: BarChart3,
        title: "Expense pattern",
        subtitle: "Utilities and subscriptions are the largest contributors.",
    },
    {
        id: "summary-3",
        icon: Bell,
        title: "Alerts",
        subtitle: "Three high-spend categories need attention.",
    },
];

export const PROGRESS_METRICS: AnalyticsMetric[] = [
    {
        icon: DollarSign,
        title: "Savings goal",
        subtitle: "65% of your quarterly savings target reached.",
        maxValue: 100,
        value: 65,
        label: "%",
        radius: 48,
    },
    {
        icon: Activity,
        title: "Budget pacing",
        subtitle: "You are on track for the current month.",
        maxValue: 100,
        value: 72,
        label: "%",
        radius: 48,
    },
];
