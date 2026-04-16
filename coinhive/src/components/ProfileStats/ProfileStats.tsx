import styles from './ProfileStats.module.css';

interface StatItem {
    icon: string;
    label: string;
    value: string;
    change: string;
    direction: 'up' | 'dn';
    variant: 'income' | 'expense' | 'net';
}

const STATS: StatItem[] = [
    { icon: '↑', label: 'Monthly Income', value: '$8,450', change: '↑ 12.4% vs last month', direction: 'up', variant: 'income' },
    { icon: '↓', label: 'Total Expenses', value: '$3,720', change: '↑ 4.1% vs last month', direction: 'dn', variant: 'expense' },
    { icon: '≈', label: 'Net Balance', value: '$4,730', change: '↑ 18.9% vs last month', direction: 'up', variant: 'net' },
];

export default function StatsGrid() {
    return (
        <div className={styles.grid}>
            {STATS.map((s) => (
                <div key={s.label} className={`${styles.box} ${styles[s.variant]}`}>
                    <div className={styles.icon}>{s.icon}</div>
                    <div className={styles.label}>{s.label}</div>
                    <div className={styles.value}>{s.value}</div>
                    <div className={`${styles.change} ${styles[s.direction]}`}>{s.change}</div>
                </div>
            ))}
        </div>
    );
}