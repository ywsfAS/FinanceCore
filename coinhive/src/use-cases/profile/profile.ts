import type { Profile } from "../../entities/profile";
import { profileService } from "../../services/profileService";

export const getProfile = async (token: string): Promise<Profile> => {
    return await profileService.fetchprofile(token)
};
export const editProfile = async (token: string, profile: Profile): Promise<void> => {
    const { firstName, lastName, bio, photo } = profile;

    await profileService.updateprofile(token, firstName, lastName, bio);

    if (photo instanceof File) {
        await profileService.uploadProfilePhoto(token, photo);
    }
};