import { useUserSummary } from '../../hooks/Reports/useUserSummary';
import styles from './ProfileStats.module.css';
import { stats } from './constants';

export default function StatsGrid() {
    const { isLoading, data, error, isError } = useUserSummary();
    //if (isLoading) return <div>loading...</div>;
    //if (isError) return <div>{error.message}</div>;
    //if (!data) return null;
    //const { totalExpense, totalIncome } = data; const netWorth = totalIncome - totalExpense;

    return (
        <div className={styles.grid}>
            {stats.map((s) => (
                <div key={s.label} className={styles.box}>
                    <div className={styles.title}>
                        <div className={styles.icon}>
                            <s.icon />
                        </div>
                        <div className={styles.label}>{s.label}</div>
                    </div>
                    <div className={styles.value}>{s.value}</div>
                    <div className={styles.change}>{s.change}</div>
                </div>
            ))}
        </div>
    );
}