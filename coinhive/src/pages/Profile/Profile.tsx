import styles from "./Profile.module.css";
import ProfileCard from "../../components/ProfileCard/ProfileCard";
import ProfileStats from "../../components/ProfileStats/ProfileStats";
import InsightsRow from "../../components/InsightRow/InsightRow";
import TransactionCard from "../../components/TransactionCard/TransactionCard";
import ChartsSection from "../../components/ChartsSection/ChartsSection";
import { useProfile } from "../../hooks/Profile/useProfile";
import { useUpdateProfile } from "../../hooks/Profile/useUpdateProfile";
import { useUploadProfileImage} from "../../hooks/Profile/useUploadProfileImage"
import ProfileEditPopUp from "../../components/ProfileEditPopUp/ProfilePopUp";
import { useState } from 'react';
import type { UpdateProfileParams, UploadProfileImageParams } from "../../services/profileService";
import { BarChartCard } from "../../components/BarChartCard/BarChartCard";

export default function ProfilePage() {

    const { data, isLoading, isError, error } = useProfile();
    const updateProfileMutation = useUpdateProfile();
    const uploadProfileImageMutation = useUploadProfileImage();
    const [active, setActive] = useState(false);
    const PopUpHandler = () => {
        setActive((prev) => !prev);
    }
    const updateProfile = async (profile : UpdateProfileParams ) => {
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


    return (
        <div className={styles.layout}>
            <ProfileCard profileData={profile} PopUpHandler={PopUpHandler} />
            <main className={styles.main}>
                <ProfileStats />
                <div className = {styles.barChartContainer}>
                    <BarChartCard/>
                </div>
                {active && <ProfileEditPopUp EditProfileImageHandler={updateProfileImage} EditProfileHandler={updateProfile} PopUpHandler={PopUpHandler} />}
            </main>
        </div>
    );
}