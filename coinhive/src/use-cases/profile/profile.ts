import type { Profile } from "../../entities/profile";
import type { Profile } from "../../entities/profile";
import { profileService } from "../../services/profileService";

export const getProfile = async (token: string): Promise<Profile> => {
    return profileService.profile(token)
};