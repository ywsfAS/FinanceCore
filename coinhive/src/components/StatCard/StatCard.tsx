import styles from './StatCard.module.css';
import { type ReactNode } from 'react';

type StatCardProps =  {
    label: string;
    value: string;
    change: string;
    direction: 'up' | 'down';
    icon: ReactNode;
    iconVariant?: 'green' | 'red' | 'blue';
}

export const StatCard = ({
    label,
    value,
    change,
    direction,
    icon,
    iconVariant = 'blue',
}: StatCardProps) => {
    return (
        <div className={styles.card}>
            <div className={styles.header}>
                <span className={styles.label}>{label}</span>
                <div className={`${styles.icon} ${styles[iconVariant]}`}>{icon}</div>
            </div>
            <div className={styles.value}>{value}</div>
            <span className={`${styles.change} ${styles[direction]}`}>
                {direction === 'up' ? '▲' : '▼'} {change}
            </span>
        </div>
    );
};