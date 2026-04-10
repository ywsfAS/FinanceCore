import type { Profile } from "../entities/profile";
const profile_URL = "https://localhost:7143/api/v1/profile";

export const profileService = {

    profile: async (token: string): Promise<Profile> => {
        const res = await fetch(profile_URL, {
            method: "GET",
            headers: {
                "Content-type": "application/json",
                Authorization: `Bearer ${token}`
        }
        });
        if (!res.ok) throw new Error("failed get profile");
        return res.json();
        
    }


}