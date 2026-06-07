import type { Profile } from "../../entities/profile";
import { getProfile, editProfile } from "../../use-cases/profile/profile"; // 1. Import editProfile here
import { useAuth } from "../Auth/Auth";
import { useState, useEffect } from "react";

export const useProfile = () => {
    const { user: { token } } = useAuth();
    const [loading, setLoading] = useState(true);
    const [updating, setUpdating] = useState(false); 
    const [profile, setProfile] = useState<Profile | null>(null);

    useEffect(() => {
        const fetchProfile = async () => {
            try {
                const data = await getProfile(token);
                setProfile(data);
            }
            catch (err) {
                console.log(err);
            }
            finally {
                setLoading(false);
            }
        }
        if (token) fetchProfile();
    }, [token]);

    const updateProfile = async (updatedData: Profile) => {
        setUpdating(true);
        try {
            await editProfile(token, updatedData);

            setProfile(updatedData);

            return { success: true };
        } catch (err) {
            console.error("Error updating profile:", err);
            return { success: false, error: err };
        } finally {
            setUpdating(false);
        }
    };

    return { profile, loading, updating, updateProfile };
};