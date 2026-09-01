import styles from "./TransactionCard.module.css";
import TransactionRow from "./TransactionRow";

import { useFiltredTransactions } from "../../hooks/Transactions/useFiltredTransactions";
import type { FiltredTransactionsParams } from "../../services/transactionService";
import { EnTransactionType, type TransactionEntity } from "../../entities/Transaction";

import { TABLE_HEADERS } from "./constants";

export interface TransactionCardProps {
    filters: FiltredTransactionsParams;
}

const formatAmount = (amount: number, type: EnTransactionType) => {
    const sign = type === EnTransactionType.Income ? "+" : "-";
    return `${sign}${amount.toFixed(2)}`;
};

const mapTransactionToUI = (tx: TransactionEntity) => ({
    id: tx.id,
    name: tx.description || tx.categoryName || "Transaction",
    account: tx.accountName || "Unkown",
    date: new Date(tx.date).toLocaleString(),
    amount: `${formatAmount(tx.amount, tx.type)} ${tx.currency}`,
    currency: tx.currency || "Unkown",
    category: tx.categoryName || "Transfer",
    positive: tx.type === EnTransactionType.Income,
});

export default function TransactionCard({ filters }: TransactionCardProps) {
    const { data, isLoading } = useFiltredTransactions(filters);

    if (isLoading) return <div className={styles.loading}>Loading transactions...</div>;

    const transactions = Array.isArray(data) ? data.map(mapTransactionToUI) : [];

    return (
        <div className={styles.card}>
            <div className={styles.header}>
                <div>
                    <h2>Recent Transactions</h2>
                    <p className={styles.subtitle}>Your latest activity, grouped for quick review.</p>
                </div>

                <button className={styles.seeAll}>See All</button>
            </div>

            <div className={styles.table}>
                <div className={styles.tableHeader}>
                    {TABLE_HEADERS.map((item) => (
                        <div key={item}>{item}</div>
                    ))}
                </div>

                {transactions.length > 0 ? (
                    transactions.map((tx) => (
                        <TransactionRow key={tx.id} transaction={tx} />
                    ))
                ) : (
                    <div className={styles.loading}>No transactions available.</div>
                )}
            </div>
        </div>
    );
}