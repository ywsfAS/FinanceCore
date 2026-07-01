import type { ProgressRingCard } from './types';
import { DEFAULT } from './constants';
import styles from './ProgressRingCard.module.css';

const ProgressRingCard = ({ maxValue = DEFAULT.maxValue, value = DEFAULT.value, title = DEFAULT.title, subtitle = DEFAULT.subtitle
    , icon: Icon = DEFAULT.icon, label = DEFAULT.label, radius = DEFAULT.radius }: ProgressRingCard) => {
    const circumference = 2 * Math.PI * radius;
    const percentage = Math.min((value / maxValue) * 100, 100);
    const strokOffset = circumference - (percentage / 100) * circumference;
    return (
        <div className={styles.card}>
            <div className={styles.header}>
                <Icon size={35} className={styles.icon} />
                <h2 className={styles.title}>{title}</h2>
            </div>
            <p className={styles.description}>{subtitle}</p>
            <div className={styles.container}>
                <svg className={styles.progressRing} >
                    <circle
                        className={styles.progressRingTrack}
                        cx={70}
                        cy={70}
                        r={radius}
                    />
                    <circle
                        className={styles.progressRingIndicator}
                        cx={70}
                        cy={70}
                        r={radius}
                        strokeDasharray={circumference}
                        strokeDashoffset={strokOffset}
                    />
                </svg>
                <div className={styles.chartLabel}>
                    <span className={styles.label}>{value}{label}</span>
                </div>
            </div>
        </div >
    );




}
export default ProgressRingCard;
