import type { Profile } from "../entities/profile";
const profile_URL = "https://localhost:7143/api/v1/profile";

export const profileService = {
    fetchprofile: async (token: string): Promise<Profile> => {
        const res = await fetch(profile_URL, {
            method: "GET",
            headers: {
                "Content-type": "application/json",
                Authorization: `Bearer ${token}`
            }
        });
        if (!res.ok) throw new Error("failed get profile");
        return res.json();
    },
    updateprofile: async (token: string, firstName: string, lastName: string, bio: string) => {
        const res = await fetch(profile_URL, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json", 
                Authorization: `Bearer ${token}`
            },
            body: JSON.stringify({
                firstName,
                lastName,
                bio,
                currency: "USD" 
            })
        });

        if (!res.ok) throw new Error("failed update profile");
        return res.json();
    },
    uploadProfilePhoto: async (token: string, photo: File) => {
        const formData = new FormData();
        formData.append("file", photo); 

        const res = await fetch(`${profile_URL}/profile-image`, { 
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}` 
            },
            body: formData
        });

        if (!res.ok) throw new Error("failed upload profile image");
        return res.json();
    }

};