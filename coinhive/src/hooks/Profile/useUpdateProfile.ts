import { useMutation, useQueryClient } from "@tanstack/react-query";
import {
    profileService,
    type UpdateProfileParams
} from "../../services/profileService";
export function useUpdateProfile() {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (profile: UpdateProfileParams) => profileService.updateProfile(profile),
        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: ["profile"]
            })
        }
    })
}