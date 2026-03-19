import {
    PieChart,
    Pie,
    Cell,
    BarChart,
    Bar,
    XAxis,
    YAxis,
    Tooltip,
    ResponsiveContainer,
} from "recharts";

import "./SpendingAnalytics.css";

const categoryData = [
    { name: "Food", value: 400 },
    { name: "Rent", value: 1200 },
    { name: "Transport", value: 300 },
    { name: "Subscriptions", value: 150 },
];

const monthlyData = [
    { month: "Jan", amount: 1200 },
    { month: "Feb", amount: 900 },
    { month: "Mar", amount: 1400 },
    { month: "Apr", amount: 1100 },
];

const COLORS = ["#4f46e5", "#22c55e", "#f59e0b", "#ef4444"];

export default function SpendingAnalytics() {
    return (
        <div className="analytics-container">
            <h3>Spending Analytics</h3>

            <div className="charts">
                {/* PIE CHART */}
                <div className="chart-card">
                    <h4>By Category</h4>
                    <ResponsiveContainer width="100%" height={250}>
                        <PieChart>
                            <Pie
                                data={categoryData}
                                dataKey="value"
                                outerRadius={80}
                            >
                                {categoryData.map((_, index) => (
                                    <Cell key={index} fill={COLORS[index]} />
                                ))}
                            </Pie>
                            <Tooltip />
                        </PieChart>
                    </ResponsiveContainer>
                </div>

                {/* BAR CHART */}
                <div className="chart-card">
                    <h4>Monthly Spending</h4>
                    <ResponsiveContainer width="100%" height={250}>
                        <BarChart data={monthlyData}>
                            <XAxis dataKey="month" />
                            <YAxis />
                            <Tooltip />
                            <Bar dataKey="amount" />
                        </BarChart>
                    </ResponsiveContainer>
                </div>
            </div>
        </div>
    );
}