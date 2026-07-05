import styles from "./Profile.module.css";
import ProfileCard from "../../components/ProfileCard/ProfileCard";
import { useProfile } from "../../hooks/Profile/useProfile";
import { useUpdateProfile } from "../../hooks/Profile/useUpdateProfile";
import { useUploadProfileImage } from "../../hooks/Profile/useUploadProfileImage"
import ProfileEditPopUp from "../../components/ProfileEditPopUp/ProfilePopUp";
import { useState } from 'react';
import type { UpdateProfileParams, UploadProfileImageParams } from "../../services/profileService";
import { EnNavLinks } from "../../components/ProfileCard/types";
import Dashboard from "../../components/Dashboard/Dashboard";
import Accounts from "../../components/Accounts/Accounts";
import Transactions from "../../components/Transactions/transactions";
import Categories from "../../components/Categories/Categories";
import Savings from "../../components/Savings/Savings";
import { PieChartCard } from "../../components/PieChartCard/PieChartCard";

export default function ProfilePage() {

    const { data, isLoading, isError, error } = useProfile();
    const updateProfileMutation = useUpdateProfile();
    const uploadProfileImageMutation = useUploadProfileImage();
    const [active, setActive] = useState(false);
    const [activeTab, setActiveTab] = useState<EnNavLinks>(EnNavLinks.Dashboard);
    const handleActiveTab = (activeTab: EnNavLinks) => {
        setActiveTab(activeTab);
    }
    const PopUpHandler = () => {
        setActive((prev) => !prev);
    }
    const updateProfile = async (profile: UpdateProfileParams) => {
        try {
            await updateProfileMutation.mutateAsync(profile);
            console.log("profile updated successfully");
        } catch (err) {
            console.error(err);
        }

    }
    const updateProfileImage = async (profile: UploadProfileImageParams) => {

        try {
            await uploadProfileImageMutation.mutateAsync(profile);
            console.log("profile image updated successfully");
        } catch (err) {
            console.error(err);
        }
    }
    //if (isLoading) return <div>Loading...</div>;
    //if (isError) return <div>{error.message}</div>;

    const profile = data;
    const DashboardContent = (() => {
        switch (activeTab) {
            case EnNavLinks.Dashboard:
                return <Dashboard />

            case EnNavLinks.Accounts:
                return <Accounts />
            case EnNavLinks.Transactions:
                return <Transactions />
            case EnNavLinks.Categories:
                return <Categories />
            case EnNavLinks.Analytics:
                return <PieChartCard title="Categories" subtitle="Analyze your spending distribution across categories" />
            case EnNavLinks.Savings:
                return <Savings />
        }
    })();
    return (
        <div className={styles.layout}>
            <ProfileCard profileData={profile} PopUpHandler={PopUpHandler} TabHandler={handleActiveTab} active={activeTab} />
            <main className={styles.main}>
                {DashboardContent}
                {active && <ProfileEditPopUp EditProfileImageHandler={updateProfileImage} EditProfileHandler={updateProfile} PopUpHandler={PopUpHandler} />}
            </main>
        </div>
    );
}