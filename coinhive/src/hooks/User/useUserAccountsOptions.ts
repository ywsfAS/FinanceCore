import { useQuery } from "@tanstack/react-query";
import {
    userService,
} from "../../services/userService";

export function useUserAccountsOptions() {
    return useQuery({
        queryKey: ["user-accounts-options"],

        queryFn: () =>
            userService.getUserAccountsOptions(),
        staleTime: 1000 * 60 * 5,
    });
}