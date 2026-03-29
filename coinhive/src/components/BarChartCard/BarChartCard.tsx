import { BarChart, Bar, XAxis, YAxis, Tooltip, CartesianGrid, ResponsiveContainer } from "recharts";
import type { MonthlyEntry, SummaryItem } from "../types";
import "../../styles/utility.css";
import { BarTooltip } from "../BarTooltip/BarTooltip";
import styles from "./BarChartCard.module.css";

type Props = {
    data: MonthlyEntry[];
    summaryItems: SummaryItem[];
};

export function BarChartCard({ data, summaryItems }: Props) {
    return (
        <div className={styles.card}>
            <p className={styles.title}>Monthly Spending</p>

            <ResponsiveContainer width="100%" height={200}>
                <BarChart data={data} barSize={36}>
                    <defs>
                        <linearGradient id="bar-gradient" x1="0" y1="0" x2="0" y2="1">
                            <stop offset="0%" stopColor="#6366f1" stopOpacity={1} />
                            <stop offset="100%" stopColor="#8b5cf6" stopOpacity={0.5} />
                        </linearGradient>
                    </defs>
                    <CartesianGrid vertical={false} stroke="rgba(255,255,255,0.04)" strokeDasharray="4 4" />
                    <XAxis dataKey="month" axisLine={false} tickLine={false} tick={{ fill: "#52525b", fontSize: 12, fontWeight: 600 }} />
                    <YAxis axisLine={false} tickLine={false} tick={{ fill: "#52525b", fontSize: 11 }} tickFormatter={(v: number) => `$${v}`} width={45} />
                    <Tooltip content={<BarTooltip />} cursor={{ fill: "rgba(255,255,255,0.03)" }} />
                    <Bar dataKey="amount" fill="url(#bar-gradient)" radius={[6, 6, 0, 0]} />
                </BarChart>
            </ResponsiveContainer>

            <div className={styles.summaryRow}>
                {summaryItems.map((item, i) => (
                    <div key={i} className={styles.summaryItem}>
                        <div className={styles.summaryLabel}>{item.label}</div>
                        <div className={styles.summaryValue}>{item.value}</div>
                    </div>
                ))}
            </div>
        </div>
    );
}