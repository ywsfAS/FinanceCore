
import "./FinancialOverview.css";


interface StatCard {
    label: string;
    value: string;
    change: string;
    direction: "up" | "down";
    icon: string;
    iconVariant: "green" | "red" | "blue";
}


const stats: StatCard[] = [
    { label: "Monthly Income", value: "$8,240", change: "+12.4%", direction: "up", icon: "?", iconVariant: "green" },
    { label: "Monthly Expenses", value: "$3,180", change: "-6.1%", direction: "down", icon: "?", iconVariant: "red" },
    { label: "Savings Rate", value: "61.4%", change: "+3.2%", direction: "up", icon: "?", iconVariant: "blue" },
];


export default function FinancialOverview() {
    return (
        <div className="fc-page">

                   <div className="fc-topbar">
                <div className="fc-topbar-left">
                    <h1>Overview</h1>
                    <p>Tuesday, April 2025</p>
                </div>
                <div className="fc-avatar">YS</div>
            </div>
            <div className="fc-hero">
                <p className="fc-hero-label">Total Balance</p>
                <p className="fc-hero-balance"><span>$</span>12,450.00</p>
                <div className="fc-hero-row">
                    <div className="fc-hero-stat">
                        <span className="fc-hero-stat-label">Income</span>
                        <span className="fc-hero-stat-value positive">+$8,240</span>
                    </div>
                    <div className="fc-hero-stat">
                        <span className="fc-hero-stat-label">Expenses</span>
                        <span className="fc-hero-stat-value negative">-$3,180</span>
                    </div>
                    <div className="fc-hero-stat">
                        <span className="fc-hero-stat-label">Net</span>
                        <span className="fc-hero-stat-value positive">+$5,060</span>
                    </div>
                </div>
            </div>

            <div className="fc-stats">
                {stats.map((s) => (
                    <div className="fc-stat-card" key={s.label}>
                        <div className="fc-stat-header">
                            <span className="fc-stat-label">{s.label}</span>
                            <div className={`fc-stat-icon ${s.iconVariant}`}>{s.icon}</div>
                        </div>
                        <div className="fc-stat-value">{s.value}</div>
                        <span className={`fc-stat-change ${s.direction}`}>
                            {s.direction === "up" ? "?" : "?"} {s.change}
                        </span>
                    </div>
                ))}
            </div>

        </div>
    );
}
