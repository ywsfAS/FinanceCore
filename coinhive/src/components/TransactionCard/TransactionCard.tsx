import styles from "./TransactionCard.module.css";
import TransactionRow from "./TransactionRow";

import { useFiltredTransactions } from "../../hooks/Transactions/useFiltredTransactions";
import type { FiltredTransactionsParams } from "../../services/transactionService";

import { TABLE_HEADERS, DEFAULT_TRANSACTIONS } from "./constants";

export default function TransactionCard(
    filters: FiltredTransactionsParams
) {
    const {
        data,
        isLoading,
    } = useFiltredTransactions(filters);

    if (isLoading) return <div className={styles.loading}>Loading transactions...</div>;

    const transactions =
        data?.length
            ? data.map((tx) => ({
                id: tx.id,
                name:
                    tx.description ??
                    tx.categoryName ??
                    "Transaction",

                account: tx.accountName ?? "Mononiex",

                date: new Date(tx.date).toLocaleString(),

                amount: `${tx.type === 1 ? "+" : "-"}${tx.amount.toFixed(2)}`,

                currency: tx.currency ?? "USD",

                category: tx.categoryName ?? "Transfer",

                positive: tx.type === 1,
            }))
            : DEFAULT_TRANSACTIONS;

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

                {transactions.map((tx) => (
                    <TransactionRow key={tx.id} transaction={tx} />
                ))}
            </div>
        </div>
    );
}