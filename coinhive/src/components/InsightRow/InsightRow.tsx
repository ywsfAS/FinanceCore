import styles from './InsightRow.module.css';

interface Insight {
    label: string;
    value: string;
    desc: string;
    pct: number;
    fillVar: string;
}
type InsightProps = {
    insights?: Insight[],
}
const INSIGHTS: Insight[] = [
    { label: 'Savings Rate', value: '56%', desc: 'Above your 50% target this month', pct: 56, fillVar: 'var(--bg-insight-fill-1)' },
    { label: 'Budget Health', value: 'Excellent', desc: '3 of 5 categories under budget', pct: 80, fillVar: 'var(--bg-insight-fill-2)' },
    { label: 'Goal Progress', value: '$142k / $200k', desc: '71% to your independence target', pct: 71, fillVar: 'var(--bg-insight-fill-3)' },
];

export default function InsightsRow({ insights = INSIGHTS }: InsightProps) {
    return (
        <div className={styles.row}>
            {insights.map(({ label, value, desc, pct, fillVar }) => (
                <div key={label} className={styles.card}>
                    <div className={styles.label}>{label}</div>
                    <div className={styles.value}>{value}</div>
                    <div className={styles.desc}>{desc}</div>
                    <div className={styles.bar}>
                        <div
                            className={styles.fill}
                            style={{ width: `${pct}%`, background: fillVar }}
                        />
                    </div>
                </div>
            ))}
        </div>
    );
}