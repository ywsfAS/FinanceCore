import styles from "./Profile.module.css";
import Logo from "../../assets/profile.jpeg";
import Card from "../../components/Card/Card";
import FinancialCard from "../../components/FinancialOverview/FinancialOverview";
import FinancialTransaction from "../../components/FinancialTransaction/FinancialTransaction";
import SpendingAnalytics from "../../components/SpendingAnalytics/SpendingAnalytics";
import { useAuth } from "../../hooks/Auth";
import { useProfile } from "../../hooks/Profile";


export default function ProfilePage() {
    const { user } = useAuth();
    const { profile } = useProfile();
    return (
        <div className={styles.container}>
            <div className={styles.left}>
                <div className={styles.user}>
                    <img
                        src={Logo}
                        alt="User Avatar"
                        className={styles.avatar}
                    />
                    <div className={styles.userInfo}>
                        <h4 className={styles.userName}>{user?.name}</h4>
                        <p className={styles.userRole}>Administrator</p>
                        <p className={styles.userBio}>
                            Passionate about web development and UI/UX design. Loves building user-friendly apps and learning new technologies.
                        </p>
                    </div>
                </div>
                <SpendingAnalytics />
            </div>
            <div className={styles.right}>
                <FinancialCard/>
                <FinancialTransaction/>
            </div>

 

        </div>
    );
}