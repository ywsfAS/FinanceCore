import { useQuery } from "@tanstack/react-query";
import {
    userService,
} from "../../services/userService";

export function useUserAccounts() {
    return useQuery({
        queryKey: ["user-accounts"],

        queryFn: () =>
            userService.getUserAccounts(),
        staleTime: 1000 * 60 * 5,
    });
}