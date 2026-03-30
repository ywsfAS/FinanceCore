import styles from "./FinancialTransaction.module.css";
import AccountDetails from "./AccountDetails/AccountDetails";
import TransactionList from "./TransactionList/TransactionList";
import type { DetailRow, Transaction } from "./types";

const details: DetailRow[] = [
    { key: "Currency", value: "USD ($)" },
    { key: "Country", value: "Morocco" },
    { key: "Linked Banks", value: "2 Accounts", pill: { label: "Verified", variant: "verified" } },
    { key: "Plan", value: "", pill: { label: "Premium", variant: "premium" } },
    { key: "Risk Profile", value: "", pill: { label: "Moderate", variant: "moderate" } },
];

const transactions: Transaction[] = [
    { icon: "💰", name: "Salary Deposit", date: "Apr 1 · 09:00", amount: "+$8,240.00", type: "credit", tag: "Salary", tagVariant: "salary" },
    { icon: "🛒", name: "Carrefour Market", date: "Apr 3 · 14:22", amount: "-$94.50", type: "debit", tag: "Shopping", tagVariant: "shopping" },
    { icon: "🍔", name: "Uber Eats", date: "Apr 4 · 20:11", amount: "-$38.00", type: "debit", tag: "Food", tagVariant: "food" },
];

export default function FinancialTransaction() {
    return (
        <div className={styles.section}>
            <h1 className={styles.title}>Transactions</h1>

            <div className={styles.grid}>
                <AccountDetails details={details} />
                <TransactionList transactions={transactions} />
            </div>
        </div>
    );
}
