import styles from "./Profile.module.css";
import ProfileCard from "../../components/ProfileCard/ProfileCard";
import ProfileStats from "../../components/ProfileStats/ProfileStats";
import InsightsRow from "../../components/InsightRow/InsightRow";
import TransactionCard from "../../components/TransactionCard/TransactionCard";
import ChartsSection from "../../components/ChartsSection/ChartsSection";
import { useProfile } from "../../hooks/Profile";
import OverviewHeader from "../../components/OverviewHeader/OverviewHeader";

export default function ProfilePage() {

    const {profile} = useProfile();
    console.log(profile);
    return (
        <div className={styles.layout}>
            <ProfileCard profileData={profile} />
            <main className={styles.main}>
                <OverviewHeader title={"Financial Overview"} description={"April 2026 · Last updated just now"} />
                <ProfileStats />
                <ChartsSection />
                <InsightsRow />
                <TransactionCard />
            </main>
        </div>
    );
}