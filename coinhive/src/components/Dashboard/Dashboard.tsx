import styles from "./Dashboard.module.css";
import ProfileStats from "../ProfileStats/ProfileStats";
import { BarChartCard } from "../BarChartCard/BarChartCard";
import  TransactionCard from "../TransactionCard/TransactionCard";
const Dashboard = () => {
    return (
       <>
         <ProfileStats />
         <div className={styles.barChartContainer}>
             <h3 className={styles.title}>Cashflow</h3>
             <p className={styles.description}>Track monthly income and expenses across the year</p>
             <BarChartCard/>
         </div>
         <TransactionCard/>
       </>
    )
}
export default Dashboard;