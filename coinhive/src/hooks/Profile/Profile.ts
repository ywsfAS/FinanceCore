import type { Profile } from "../../entities/profile";
import { getProfile } from "../../use-cases/profile/profile";
import { useAuth } from "../Auth/Auth";
import { useState, useEffect } from "react";


export const useProfile = () => {
    const { user: { token } } = useAuth();
    const [loading, setLoading] = useState(true);
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
        if(token) fetchProfile();
    }, [token])
    return { profile, loading };
}