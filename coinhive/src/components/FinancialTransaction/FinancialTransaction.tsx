import "./FinancialTransaction.css";

interface DetailRow {
    key: string;
    value: string;
    pill?: { label: string; variant: "premium" | "moderate" | "verified" };
}

interface Transaction {
    icon: string;
    name: string;
    date: string;
    amount: string;
    type: "credit" | "debit";
    tag: string;
    tagVariant: "food" | "salary" | "transport" | "shopping" | "subscript" | "transfer";
}

const details: DetailRow[] = [
    { key: "Currency",     value: "USD ($)" },
    { key: "Country",      value: "Morocco" },
    { key: "Linked Banks", value: "2 Accounts", pill: { label: "Verified", variant: "verified" } },
    { key: "Plan",         value: "",           pill: { label: "Premium",  variant: "premium"  } },
    { key: "Risk Profile", value: "",           pill: { label: "Moderate", variant: "moderate" } },
];

const transactions: Transaction[] = [
    { icon: "??", name: "Salary Deposit",   date: "Apr 1 · 09:00", amount: "+$8,240.00", type: "credit", tag: "Salary",    tagVariant: "salary"    },
    { icon: "??", name: "Carrefour Market", date: "Apr 3 · 14:22", amount: "-$94.50",    type: "debit",  tag: "Shopping",  tagVariant: "shopping"  },
    { icon: "??", name: "Uber Eats",        date: "Apr 4 · 20:11", amount: "-$38.00",    type: "debit",  tag: "Food",      tagVariant: "food"      },
    { icon: "??", name: "Casablanca Tram",  date: "Apr 5 · 08:05", amount: "-$7.20",     type: "debit",  tag: "Transport", tagVariant: "transport" },
    { icon: "??", name: "Netflix",          date: "Apr 6 · 00:00", amount: "-$15.99",    type: "debit",  tag: "Subscript", tagVariant: "subscript" },
    { icon: "??", name: "Savings Transfer", date: "Apr 7 · 11:30", amount: "-$500.00",   type: "debit",  tag: "Transfer",  tagVariant: "transfer"  },
];
export default function FinancialTransaction() {
    return (
        <div className="fb-section">
            <h1 className="title">Transactions</h1>
            <div className="fb-grid">

                {/* Account Details */}
                <div className="fb-card">
                    <p className="fb-section-title">Account Details</p>
                    <div className="fb-detail-list">
                        {details.map((d) => (
                            <div className="fb-detail-row" key={d.key}>
                                <div className="fb-detail-key">
                                    <span className="fb-detail-dot" />
                                    {d.key}
                                </div>
                                {d.pill ? (
                                    <span className={`fb-pill ${d.pill.variant}`}>{d.pill.label}</span>
                                ) : (
                                    <span className="fb-detail-val">{d.value}</span>
                                )}
                            </div>
                        ))}
                    </div>
                </div>

                {/* Recent Transactions */}
                <div className="fb-card">
                    <div className="fb-tx-header">
                        <p className="fb-section-title" style={{ margin: 0 }}>Recent Transactions</p>
                        <button className="fb-tx-link">View all ?</button>
                    </div>
                    <div className="fb-tx-list">
                        {transactions.map((tx) => (
                            <div className="fb-tx-row" key={tx.name + tx.date}>
                                <div className="fb-tx-icon">{tx.icon}</div>
                                <div className="fb-tx-info">
                                    <div className="fb-tx-name">{tx.name}</div>
                                    <div className="fb-tx-date">{tx.date}</div>
                                </div>
                                <span className={`fb-tx-tag ${tx.tagVariant}`}>{tx.tag}</span>
                                <span className={`fb-tx-amount ${tx.type}`}>{tx.amount}</span>
                            </div>
                        ))}
                    </div>
                </div>

            </div>
        </div>
    );
}
