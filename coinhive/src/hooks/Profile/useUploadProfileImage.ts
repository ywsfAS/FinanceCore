import { useMutation } from "@tanstack/react-query";
import {
    profileService,
    type UploadProfileImageParams
} from "../../services/profileService";
export function useUploadProfileImage() {
    return useMutation({
        mutationFn: (profileImage : UploadProfileImageParams) => profileService.uploadProfilePhoto(profileImage),
    })
}