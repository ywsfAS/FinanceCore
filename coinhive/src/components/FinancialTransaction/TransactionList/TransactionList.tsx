import styles from "./TransactionList.module.css";
import type { Transaction } from "../types";

type Props = {
    transactions: Transaction[];
};

export default function TransactionList({ transactions }: Props) {
    return (
        <div className={styles.card}>
            <div className={styles.header}>
                <p className={styles.title}>Recent Transactions</p>
                <button className={styles.link}>View all →</button>
            </div>

            <div className={styles.list}>
                {transactions.map((tx) => (
                    <div className={styles.row} key={tx.name + tx.date}>
                        <div className={styles.icon}>{tx.icon}</div>

                        <div className={styles.info}>
                            <div className={styles.name}>{tx.name}</div>
                            <div className={styles.date}>{tx.date}</div>
                        </div>

                        <span className={`${styles.tag} ${styles[tx.tagVariant]}`}>
                            {tx.tag}
                        </span>

                        <span className={`${styles.amount} ${styles[tx.type]}`}>
                            {tx.amount}
                        </span>
                    </div>
                ))}
            </div>
        </div>
    );
}