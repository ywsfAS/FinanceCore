import { PieChartCard } from "../PieChartCard/PieChartCard";
import { BarChartCard } from "../BarChartCard/BarChartCard";
import type { CategoryEntry, MonthlyEntry, SummaryItem } from "../types";
import styles from "./SpendingAnalytics.module.css";

const categoryData: CategoryEntry[] = [
    { name: "Food", value: 400 },
    { name: "Rent", value: 1200 },
    { name: "Transport", value: 300 },
    { name: "Subscriptions", value: 150 },
];

const monthlyData: MonthlyEntry[] = [
    { month: "Jan", amount: 1200 },
    { month: "Feb", amount: 900 },
    { month: "Mar", amount: 1400 },
    { month: "Apr", amount: 1100 },
];

const COLORS: string[] = ["#6366f1", "#10b981", "#f59e0b", "#f43f5e"];
const CATEGORY_ICONS: string[] = ["🍔", "🏠", "🚌", "📦"];

const summaryItems: SummaryItem[] = [
    { label: "Avg / Month", value: `$${Math.round(monthlyData.reduce((s, d) => s + d.amount, 0) / monthlyData.length)}` },
    { label: "Peak Month", value: "Mar" },
    { label: "Total", value: `$${monthlyData.reduce((s, d) => s + d.amount, 0).toLocaleString()}` },
];

export default function SpendingAnalytics() {
    return (
        <div className={styles.wrapper}>
            <div className={styles.header}>
                <h2 className={styles.title}>Spending Analytics</h2>
                <span className={styles.badge}>April 2025</span>
            </div>

            <div className={styles.grid}>
                <PieChartCard data={categoryData} colors={COLORS} icons={CATEGORY_ICONS} />
                <BarChartCard data={monthlyData} summaryItems={summaryItems} />
            </div>
        </div>
    );
}