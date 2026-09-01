import styles from "./Dashboard.module.css";
import ProfileStats from "../ProfileStats/ProfileStats";
import { BarChartCard } from "../BarChartCard/BarChartCard";
import TransactionCard from "../TransactionCard/TransactionCard";
import { useUserMonthlyTrend } from "../../hooks/Reports/useUserMonthlyTrend";
import type { FiltredTransactionsParams } from "../../services/transactionService";
const Dashboard = () => {
    const { data, isLoading, isError, error } = useUserMonthlyTrend({ month: 5 });
    const filters: FiltredTransactionsParams = {
        Page: 1,
        PageSize: 5
    }
    if (isLoading) return <div>loading...</div>;
    if (isError) return <div>{error.message}</div>;
    return (
        <>
            <ProfileStats />
            <div className={styles.barChartContainer}>
                <h3 className={styles.title}>Cashflow</h3>
                <p className={styles.description}>Track monthly income and expenses across the year</p>
                <BarChartCard data={data} label="month" dataKey1="totalIncome" dataKey2="totalExpense" />
            </div>
            <TransactionCard filters={filters} />
        </>
    )
}
export default Dashboard;