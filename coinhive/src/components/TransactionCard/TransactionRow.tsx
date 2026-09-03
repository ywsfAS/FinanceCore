import styles from "./TransactionCard.module.css";
import { TRANSACTION_TYPE_ICONS } from "./constants";
import TransactionContextMenu from "../TransactionContextMenu/TransactionContextMenu";
import type { TransactionUI } from "./types";

interface Props {
    transaction: TransactionUI;
    menuOpen: boolean;
    onMenuOpen: (id: string) => void;
    onMenuClose: () => void;
    onRemove: () => void;
    onImport: () => void;
    onView: () => void;
}

export default function TransactionRow({
    transaction,
    menuOpen,
    onMenuOpen,
    onMenuClose,
    onImport,
    onView,
    onRemove


}: Props) {
    const TransactionIcon = TRANSACTION_TYPE_ICONS[transaction.type as keyof typeof TRANSACTION_TYPE_ICONS] ?? TRANSACTION_TYPE_ICONS.Expense;
    return (
        <div className={styles.row} onContextMenu={(event) => { event.preventDefault(); onMenuOpen(transaction.id); }}>
            <div className={styles.iconWrapper}>
                <TransactionIcon size={18} />
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

            <div className={styles.status}>Completed</div>
            {menuOpen && <TransactionContextMenu onClose={onMenuClose} onDelete={onRemove} onImport={onImport} onView={onView} />}
        </div>
    );
}