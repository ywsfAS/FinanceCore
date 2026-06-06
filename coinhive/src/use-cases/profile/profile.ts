import type { Profile } from "../../entities/profile";
import { profileService } from "../../services/profileService";

export const getProfile = async (token: string): Promise<Profile> => {
    return await profileService.fetchprofile(token)
};
export const editProfile = async (token: string, profile: Profile): Promise<void> => {
    const { firstName, lastName, bio, photo } = profile;

    // 1. Update text fields via PUT (JSON) -> Hits [HttpPut] UpdateProfile
    await profileService.updateprofile(token, firstName, lastName, bio);

    // 2. If a new file exists, upload it via POST (FormData) -> Hits [HttpPost("profile-image")]
    if (photo instanceof File) {
        await profileService.uploadProfilePhoto(token, photo);
    }
};