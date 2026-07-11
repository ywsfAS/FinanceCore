import type { AnalyticsStat } from "./types";
import styles from "./SummaryCard.module.css";

const SummaryCard = ({ icon: Icon, title, subtitle }: AnalyticsStat) => {
    return (
        <div className={styles.card}>
            <div className={styles.iconWrapper}>
                <Icon size={20} />
            </div>
            <div>
                <h3 className={styles.title}>{title}</h3>
                <p className={styles.subtitle}>{subtitle}</p>
            </div>
        </div>
    );
};

export default SummaryCard;
