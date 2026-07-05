import type { SavingGoal } from "./types";
import styles from "./SavingGoalCard.module.css";

type SavingGoalCardProps = {
    goal: SavingGoal;
};

const SavingGoalCard = ({ goal }: SavingGoalCardProps) => {
    const progress = Math.min(100, Math.round((goal.currentAmount / goal.targetAmount) * 100));

    return (
        <div className={styles.card}>
            <div className={styles.header}>
                <div>
                    <h3>{goal.name}</h3>
                    <p>{goal.description}</p>
                </div>
                <span className={styles.status}>{goal.status}</span>
            </div>
            <div className={styles.details}>
                <div>{goal.currency} {goal.currentAmount.toLocaleString()}</div>
                <div>Target: {goal.currency} {goal.targetAmount.toLocaleString()}</div>
                <div>By {goal.targetDate}</div>
            </div>
            <div className={styles.progressBar}>
                <div className={styles.progress} style={{ width: `${progress}%` }} />
            </div>
            <div className={styles.footer}>{progress}% complete</div>
        </div>
    );
};

export default SavingGoalCard;
