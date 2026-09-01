import { useMutation, useQueryClient } from "@tanstack/react-query";
import {
    profileService,
    type UploadProfileImageParams
} from "../../services/profileService";
export function useUploadProfileImage() {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (profileImage: UploadProfileImageParams) => profileService.uploadProfilePhoto(profileImage),
        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: ["profile"]
            })
        }
    })
}