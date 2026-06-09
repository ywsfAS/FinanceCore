import styles from './TransactionCard.module.css';
import { CircleArrowRight, BanknoteArrowDown, BanknoteArrowUp } from 'lucide-react';
import { useFiltredTransactions } from '../../hooks/Transactions/useFiltredTransactions';
import type { FiltredTransactionsParams } from '../../services/transactionService';

export default function TransactionCard(filters: FiltredTransactionsParams) {
    const { data, isLoading, isError, error } = useFiltredTransactions(filters);
    if (isLoading) return <div>Loading...</div>;
    if (isError) return <div>{error.message}</div>;
    if (!data || data.length === 0) return <div>No transactions</div>;

    const transactionsUI = data.map(tx => ({
        id: tx.id,
        name: tx.description ?? tx.categoryName ?? 'Transaction',
        date: new Date(tx.date).toLocaleDateString(),
        amount: tx.amount.toFixed(2),
        currency : tx.currency ?? 'not provided',
        category: tx.categoryName ?? 'transfer',
        positive: tx.type === 1,
        sign: tx.type === 1 ? '+' : '−',
        bgVar: tx.type === 1 ? '#dcfce7' : '#fee2e2'
    }));
    const transactionIcon = (isIncome : boolean) => {
        if (isIncome) return <BanknoteArrowUp />;
        return <BanknoteArrowDown />;
    }
    return (
        <div className={styles.card}>
            <div className={styles.header}>
                <span className={styles.title}>Recent Transactions</span>
                <CircleArrowRight className={ styles.viewAll} size={20} />
            </div>

            {transactionsUI.map((tx) => (
                <div key={tx.id} className={styles.item}>
                    <div className={styles.ico} style={{ background: tx.bgVar }}>
                        {transactionIcon(tx.positive)}
                    </div>

                    <div className={styles.meta}>
                        <div className={styles.name}>{tx.name}</div>
                        <div className={styles.date}>{tx.date}</div>
                    </div>

                    <div className={styles.right}>
                        <div className={`${styles.amount} ${tx.positive ? styles.pos : styles.neg}`}>
                            {tx.sign}{tx.amount}<span className={styles.currency}>{tx.currency}</span>
                        </div>
                        <div className={styles.cat}>{tx.category}</div>
                    </div>
                </div>
            ))}
        </div>
    );
}