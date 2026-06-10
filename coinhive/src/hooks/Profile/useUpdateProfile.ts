import { useMutation } from "@tanstack/react-query";
import {
    profileService,
    type UpdateProfileParams
} from "../../services/profileService";
export function useUpdateProfile() {
    return useMutation({
        mutationFn: (profile : UpdateProfileParams) => profileService.updateProfile(profile),
    })
}