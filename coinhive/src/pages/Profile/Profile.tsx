import styles from "./Profile.module.css";
import ProfileCard from "../../components/ProfileCard/ProfileCard";
import ProfileStats from "../../components/ProfileStats/ProfileStats";
import InsightsRow from "../../components/InsightRow/InsightRow";
import { useAuth } from "../../hooks/Auth";
import { useProfile } from "../../hooks/Profile";
import TransactionCard from "../../components/TransactionCard/TransactionCard";
import ChartsSection from "../../components/ChartsSection/ChartsSection";
import OverviewHeader from "../../components/OverviewHeader/OverviewHeader";

export default function ProfilePage() {
    //const { profile } = useProfile();
    //const { theme } = useTheme();
    //console.log(profile);
    return (
        <div className={styles.layout}>
            <ProfileCard />
            <main className={styles.main}>
                <OverviewHeader />
                <ProfileStats />
                <ChartsSection />
                <InsightsRow />
                <TransactionCard />
            </main>
        </div>
    );
}