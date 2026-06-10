import {apiClient} from "../lib/apiClient";

export interface UpdateProfileParams {
    firstName: string;
    lastName: string;
    bio: string;
}

export interface UploadProfileImageParams {
    photo : File;
}

export const profileService = {
    getProfile: async () => {
        return apiClient(`/profile`);
    },
    updateProfile: async (profile: UpdateProfileParams) => {
        return apiClient(`/profile`, {
            method: 'PUT',
            body : JSON.stringify(profile)
        })
    },
    uploadProfilePhoto: async (photo : UploadProfileImageParams) => {
        return apiClient(`/profile/profile-image`, {
            method: 'POST',
            body : JSON.stringify(photo)
        })
    }

};