import styles from './BudgetCard.module.css';

interface BudgetCardProps {
    id: string;
    name: string;
    amount: number;
    currency: string;
    categoryName: string;
    period: string;
    startDate: Date;
    endDate: Date;
    onEdit?: (id: string) => void;
    onDelete?: (id: string) => Promise<void>;
}

const BudgetCard = ({
    id,
    name,
    amount,
    currency,
    categoryName,
    period,
    startDate,
    endDate,
    onEdit,
    onDelete
}: BudgetCardProps) => {
    return (
        <div className={styles.card}>
            <div className={styles.info}>
                <h3>{name}</h3>

                <span className={styles.category}>
                    {categoryName}
                </span>

                <span className={styles.period}>
                    {period}
                </span>

                <h2>
                    {currency} {amount.toLocaleString()}
                </h2>

                <p>
                    {startDate.toISOString()} → {endDate.toISOString()}
                </p>
            </div>

            <div className={styles.actions}>
                <button
                    className={styles.editBtn}
                    onClick={() => onEdit?.(id)}
                >
                    Edit
                </button>

                <button
                    className={styles.deleteBtn}
                    onClick={() => onDelete?.(id)}
                >
                    Delete
                </button>
            </div>
        </div>
    );
};

export default BudgetCard;