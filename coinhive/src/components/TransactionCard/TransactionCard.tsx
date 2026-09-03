import styles from "./TransactionCard.module.css";
import { useEffect, useState } from "react";
import TransactionRow from "./TransactionRow";

import { useFiltredTransactions } from "../../hooks/Transactions/useFiltredTransactions";
import type { DeleteTransactionParams, FiltredTransactionsParams, ImportTransactionsParams } from "../../services/transactionService";
import { EnTransactionType, type TransactionEntity } from "../../entities/Transaction";
import { useRemoveTransaction } from '../../hooks/Transactions/useDeleteTransaction';
import { useImportTransaction } from '../../hooks/Transactions/useImportTransaction';
import { TransactionAction, type TransactionActions } from '../TransactionActionPopUp/TransactionAction';

import { TABLE_HEADERS } from "./constants";

export interface TransactionCardProps {
    filters: FiltredTransactionsParams;
    onPageAvailabilityChange?: (hasNextPage: boolean) => void;
    onLoadingChange?: (isLoading: boolean) => void;
    title?: string;
    description?: string;
    onSeeAll?: () => void;
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
    type: tx.type,
});

export default function TransactionCard({ filters, onPageAvailabilityChange, onLoadingChange, title = "Recent Transactions", onSeeAll, description = "Your latest activity, grouped for quick review." }: TransactionCardProps) {
    const { data, isLoading } = useFiltredTransactions(filters);
    const transactions = Array.isArray(data) ? data.map(mapTransactionToUI) : [];

    const [selectedTransaction, setSelectedTransaction] = useState<string | null>(null);
    const [action, setAction] = useState<TransactionActions | null>(null);
    const [openMenuId, setOpenMenuId] = useState<string | null>(null);
    const openAction = (action: TransactionActions) => {
        setAction(action);
    }
    const closeAction = () => {
        setAction(null);
    }
    useEffect(() => {
        onLoadingChange?.(isLoading);
        if (!isLoading) onPageAvailabilityChange?.(transactions.length === (filters.PageSize ?? 5));
    }, [filters.PageSize, isLoading, onLoadingChange, onPageAvailabilityChange, transactions.length]);

    if (isLoading) return <div className={styles.loading}>Loading transactions...</div>;

    return (
        <div className={styles.card} onClick={() => setOpenMenuId(null)}>
            <div className={styles.header}>
                <div>
                    <h2>{title}</h2>
                    <p className={styles.subtitle}>{description}</p>
                </div>

                {onSeeAll && <button type="button" className={styles.seeAll} onClick={onSeeAll}>See All</button>}
            </div>

            <div className={styles.table}>
                <div className={styles.tableHeader}>
                    {TABLE_HEADERS.map((item) => (
                        <div key={item}>{item}</div>
                    ))}
                </div>

                {transactions.length > 0 ? (
                    transactions.map((tx) => (
                        <TransactionRow
                            onImport={() => {
                                setSelectedTransaction(tx.id);
                                openAction("import");
                            }}
                            onView={() => {
                                setSelectedTransaction(tx.id);
                                openAction("id");
                            }}
                            onRemove={() => {
                                setSelectedTransaction(tx.id);
                                openAction("remove");

                            }}
                            onMenuOpen={() => setOpenMenuId(tx.id)}
                            onMenuClose={() => setOpenMenuId(null)}
                            key={tx.id}
                            transaction={tx}
                            menuOpen={openMenuId === tx.id}
                        />
                    ))
                ) : (
                    <div className={styles.loading}>No transactions available.</div>
                )}
            </div>
            {action && selectedTransaction &&
                <TransactionAction
                    action={action}
                    id={selectedTransaction}
                    onClose={closeAction}
                />}
        </div>
    );
}