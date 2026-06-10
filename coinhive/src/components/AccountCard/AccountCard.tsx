import styles from './AccountCard.module.css';

interface AccountCardProps {
    id: string;
    name: string;
    type: string;
    balance: number;
    currency: string;
    onView?: (id: string) => void;
}

const AccountCard = ({
    id,
    name,
    type,
    balance,
    currency,
    onView
}: AccountCardProps) => {
    return (
        <div className={styles.card}>
            <div className={styles.top}>
                <h3>{name}</h3>
                <span>{type}</span>
            </div>

            <div className={styles.currency}>
                {currency}
            </div>

            <div className={styles.balanceContainer}>
                <span>Balance</span>
                <h2>
                    {balance.toLocaleString()}
                </h2>
            </div>

            <button
                className={styles.btn}
                onClick={() => onView?.(id)}
            >
                View Details
            </button>
        </div>
    );
};

export default AccountCard;