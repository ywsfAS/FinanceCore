import styles from "./Profile.module.css";
import Logo from "../../assets/profile.jpeg";
import Card from "../../components/Card/Card";
import FinancialCard from "../../components/FinancialOverview/FinancialOverview";
import FinancialTransaction from "../../components/FinancialTransaction/FinancialTransaction";
import SpendingAnalytics from "../../components/SpendingAnalytics/SpendingAnalytics";
import { useAuth } from "../../hooks/Auth";
import { useProfile } from "../../hooks/Profile";
import { useTheme } from "../../hooks/Theme";


export default function ProfilePage() {
    const { profile } = useProfile();
    const { theme } = useTheme();
    console.log(profile);
    return (
        <div className={`${ styles.container } ${ theme === 'dark' && styles.dark}`}>
            <div className={styles.left}>
                <Card theme={theme} variant='col'>
                    <img
                        src={Logo}
                        alt="User Avatar"
                        className={styles.avatar}
                    />
                    <div className={styles.userInfo}>
                        <h4 className={styles.userName}>{`${profile?.firstName} ${profile?.lastName}`}</h4>
                        <p className={styles.userRole}>Administrator</p>
                        <p className={styles.userBio}>
                            {profile?.bio }  
                        </p>
                    </div>
                </Card>
                <SpendingAnalytics />
            </div>
            <div className={styles.right}>
                <FinancialCard/>
                <FinancialTransaction/>
            </div>

 

        </div>
    );
}