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

            <div>{transaction.name}</div>

            <div>{transaction.account}</div>

            <div>{transaction.date}</div>

            <div
                className={
                    transaction.positive
                        ? styles.positive
                        : styles.negative
                }
            >
                {transaction.amount}
            </div>

            <div className={styles.status}>
                Completed
            </div>
        </div>
    );
}