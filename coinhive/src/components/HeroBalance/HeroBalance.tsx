import styles from './HeroBalance.module.css';

type StatMini =  {
    label: string;
    value: string;
    positive?: boolean;
}

type HeroBalanceProps = {
    title: string;
    totalBalance: string;
    stats: StatMini[];
}

export const HeroBalance = ({ title, totalBalance, stats }: HeroBalanceProps) => {
    return (
        <div className={styles.hero}>
            <p className={styles['hero-label']}>{title}</p>
            <p className={styles['hero-balance']}>
                <span>$</span>{totalBalance}
            </p>
            <div className={styles['hero-row']}>
                {stats.map((s) => (
                    <div key={s.label} className={styles['hero-stat']}>
                        <span className={styles['hero-stat-label']}>{s.label}</span>
                        <span
                            className={`${styles['hero-stat-value']} ${s.positive ? styles.positive : styles.negative}`}
                        >
                            {s.positive ? '+' : '-'}{s.value}
                        </span>
                    </div>
                ))}
            </div>
        </div>
    );
};