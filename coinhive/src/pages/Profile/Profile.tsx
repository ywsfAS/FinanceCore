import styles from "./Profile.module.css";
import ProfileCard from "../../components/ProfileCard/ProfileCard";
import ProfileStats from "../../components/ProfileStats/ProfileStats";
import InsightsRow from "../../components/InsightRow/InsightRow";
import TransactionCard from "../../components/TransactionCard/TransactionCard";
import ChartsSection from "../../components/ChartsSection/ChartsSection";
import { useProfile } from "../../hooks/Profile/Profile";
import OverviewHeader from "../../components/OverviewHeader/OverviewHeader";
import ProfileEditPopUp from "../../components/ProfileEditPopUp/ProfilePopUp";
import { useState } from 'react';

export default function ProfilePage() {

    const { profile, updateProfile } = useProfile();
    const [active, setActive] = useState(false);
    const PopUpHandler = () => {
        setActive((prev) => !prev);
    }

    return (
        <div className={styles.layout}>
            <ProfileCard profileData={profile} PopUpHandler={PopUpHandler} />
            <main className={styles.main}>
                <OverviewHeader title={"Financial Overview"} description={"April 2026 · Last updated just now"} />
                <ProfileStats />
                <ChartsSection />
                <InsightsRow />
                <TransactionCard />
                {active && <ProfileEditPopUp EditProfileHandler={updateProfile} PopUpHandler={PopUpHandler} />}
            </main>
        </div>
    );
}