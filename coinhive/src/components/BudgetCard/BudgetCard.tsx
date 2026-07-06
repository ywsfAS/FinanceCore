import { CreditCard, Pencil, Trash2 } from 'lucide-react';
import Button from '../Button/Button';
import styles from './BudgetCard.module.css';
import type { BudgetCardProps } from './types';

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
    onDelete,
}: BudgetCardProps) => {
    return (
        <article className={styles.card} id={id}>
            <div className={styles.topRow}>
                <div className={styles.iconWrapper}>
                    <CreditCard size={30} />
                </div>

                <div className={styles.meta}>
                    <h3>{name}</h3>
                    <span className={styles.category}>{categoryName}</span>
                </div>

                <span className={styles.period}>{period}</span>
            </div>

            <div className={styles.amountRow}>
                <p className={styles.amount}>
                    {currency} {amount.toLocaleString()}
                </p>
                <span className={styles.label}>
                    Start : {startDate}
                </span>
                <span className={styles.label}>
                    End : {endDate}
                </span>
            </div>

            <div className={styles.actions}>
                <Button size="small" variant="ghost" onClick={() => onEdit?.(id)}>
                    <Pencil size={14} className={styles.icon} />
                    Edit
                </Button>
                <Button size="small" variant="ghost" onClick={() => onDelete?.(id)}>
                    <Trash2 size={14} className={styles.icon} />
                    Delete
                </Button>
            </div>
        </article>
    );
};

export default BudgetCard;