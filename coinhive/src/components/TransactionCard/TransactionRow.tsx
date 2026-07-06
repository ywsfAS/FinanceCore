import styles from "./TransactionCard.module.css";
import { getTransactionIcon } from "./constants";
import type { TransactionUI } from "./types";

interface Props {
    transaction: TransactionUI;
}

export default function TransactionRow({
    transaction,
}: Props) {
    return (
        <div className={styles.row}>
            <div className={styles.iconWrapper}>
                {getTransactionIcon(transaction.positive)}
            </div>

            <div className={styles.nameBlock}>
                <span className={styles.name}>{transaction.name}</span>
                <span className={styles.category}>{transaction.category}</span>
            </div>

            <div className={styles.account}>{transaction.account}</div>

            <div className={styles.date}>{transaction.date}</div>

            <div
                className={`${styles.amount} ${transaction.positive ? styles.positive : styles.negative}`}
            >
                {transaction.amount}
            </div>

            <div className={styles.statusPill}>Completed</div>
        </div>
    );
}