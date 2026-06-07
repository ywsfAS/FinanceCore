import { useUserSummary } from '../../hooks/Reports/useUserSummary';
import styles from './ProfileStats.module.css';

interface StatItem {
    icon: string;
    label: string;
    value: string;
    change: string;
    direction: 'up' | 'dn';
    variant: 'income' | 'expense' | 'net';
}
export default function StatsGrid() {
    const { isLoading, data, error, isError } = useUserSummary();
    if (isLoading) return <div>loading...</div>;
    if (isError) return <div>{error.message}</div>;
    if (!data) return null;
    const { totalExpense, totalIncome } = data;
    const netWorth = totalIncome - totalExpense;

    const stats: StatItem[] = [
        { icon: '↑', label: 'Monthly Income', value: totalIncome, change: '↑ 12.4% vs last month', direction: 'up', variant: 'income' },
        { icon: '↓', label: 'Total Expenses', value: totalExpense, change: '↑ 4.1% vs last month', direction: 'dn', variant: 'expense' },
        { icon: '≈', label: 'Net Balance', value: netWorth, change: '↑ 18.9% vs last month', direction: 'up', variant: 'net' },
    ];

    return (
        <div className={styles.grid}>
            {stats.map((s) => (
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