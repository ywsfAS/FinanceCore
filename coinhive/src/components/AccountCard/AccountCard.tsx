import styles from './AccountCard.module.css';
import { CreditCard, ArrowUpFromDot } from "lucide-react";
import type {AccountCardProps} from "./types"

const AccountCard = ({
    id,
    name,
    type,
    balance,
    currency,
    label,
    onView
}: AccountCardProps) => {
    return (
        <div className={styles.card} id={id}>
            <div className={styles.top}>
                <CreditCard /> 
                <h3>{name}</h3>
            </div>
            <span className={styles.type}>{type}</span>
            <div className={styles.balance }>
                {balance.toLocaleString()}
                <span className={styles.currency }>{currency}</span>
            </div>
            <div className={styles.description}>
                <ArrowUpFromDot size={15} />    
                <span className={styles.label}>{label }</span>
            </div>
        </div>
    );
};

export default AccountCard;