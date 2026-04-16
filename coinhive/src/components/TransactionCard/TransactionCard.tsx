import styles from './TransactionCard.module.css';

interface Transaction {
    id: number;
    name: string;
    date: string;
    amount: string;
    positive: boolean;
    category: string;
    bgVar: string;
    sign: string;
}

const TRANSACTIONS: Transaction[] = [
    { id: 1, name: 'Salary Deposit', date: 'Apr 1, 2026 · Direct deposit', amount: '$8,450', positive: true, category: 'Income', bgVar: 'var(--bg-tx-income)', sign: '+' },
    { id: 2, name: 'Rent Payment', date: 'Apr 2, 2026 · Bank transfer', amount: '$1,400', positive: false, category: 'Housing', bgVar: 'var(--bg-tx-expense)', sign: '−' },
    { id: 3, name: 'Grocery Store', date: 'Apr 4, 2026 · Card purchase', amount: '$187', positive: false, category: 'Food', bgVar: 'var(--bg-tx-expense)', sign: '−' },
    { id: 4, name: 'Freelance Invoice', date: 'Apr 8, 2026 · Wire transfer', amount: '$650', positive: true, category: 'Income', bgVar: 'var(--bg-tx-income)', sign: '+' },
    { id: 5, name: 'Netflix / Spotify', date: 'Apr 10, 2026 · Subscription', amount: '$28', positive: false, category: 'Subs', bgVar: 'var(--bg-tx-expense)', sign: '−' },
];

export default function TransactionCard() {
    return (
        <div className={styles.card}>
            <div className={styles.header}>
                <span className={styles.title}>Recent Transactions</span>
                <span className={styles.viewAll}>View all →</span>
            </div>

            {TRANSACTIONS.map((tx) => (
                <div key={tx.id} className={styles.item}>
                    <div className={styles.ico} style={{ background: tx.bgVar }}>
                        {tx.sign}
                    </div>
                    <div className={styles.meta}>
                        <div className={styles.name}>{tx.name}</div>
                        <div className={styles.date}>{tx.date}</div>
                    </div>
                    <div className={styles.right}>
                        <div className={`${styles.amount} ${tx.positive ? styles.pos : styles.neg}`}>
                            {tx.positive ? '+' : '−'}{tx.amount}
                        </div>
                        <div className={styles.cat}>{tx.category}</div>
                    </div>
                </div>
            ))}
        </div>
    );
}