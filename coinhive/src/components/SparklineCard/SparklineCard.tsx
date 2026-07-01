import styles from "./SparklineCard.module.css";
import { DEFAULT } from './constants';
import type { SparklineCard } from './types';


const SparklineCard = ({ title = DEFAULT.title, subtitle = DEFAULT.subtitle, icon: Icon = DEFAULT.icon, id }: SparklineCard) => {

    const fillId = `vertical-gradient-${id}`
    return (
        <div className={styles.card}>
            <div className={styles.header}>
                <Icon size={35} className={styles.icon} />
                <h2 className={styles.title}>{title}</h2>
            </div>
            <p className={styles.description}>{subtitle}</p>
            <div className={styles.container}>
                <svg className={styles.waveContainer}>
                    <defs>
                        <linearGradient id={fillId} x1='0' y1='0' x2='0' y2='1'>
                            <stop offset='0%' stopColor="var(--primary-50)" />
                            <stop offset='20%' stopColor="var(--primary-100)" />
                            <stop offset='40%' stopColor="var(--primary-200)" />
                            <stop offset='60%' stopColor="var(--primary-300)" />
                            <stop offset='80%' stopColor="var(--primary-400)" />
                            <stop offset='100%' stopColor="var(--primary-500)" />
                        </linearGradient>
                    </defs>

                    <path
                        className={styles.waveArea}
                        fill={`url(#${fillId})`}
                        d="M 0 45 
                    C 20 40, 30 55, 50 50 
                    C 70 45, 80 25, 100 45 
                    C 120 65, 130 35, 150 40 
                    C 170 45, 180 20, 200 35 
                    L 200 104 
                    A 16 16 0 0 1 184 120 
                    L 16 120 
                    A 16 16 0 0 1 0 104 
                    Z"
                    />

                    <path
                        d="M 0 45 C 20 40, 30 55, 50 50 C 70 45, 80 25, 100 45 C 120 65, 130 35, 150 40 C 170 45, 180 20, 200 35"
                        fill="none"
                        className={styles.waveStrok}
                    />

                </svg>
            </div>
        </div >
    )


}
export default SparklineCard;