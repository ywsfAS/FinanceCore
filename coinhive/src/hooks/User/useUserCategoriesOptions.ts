import { useQuery } from "@tanstack/react-query";
import {
    userService,
} from "../../services/userService";

export function useUserCategoriesOptions() {
    return useQuery({
        queryKey: ["user-categories-options"],

        queryFn: () =>
            userService.getUserCategoriesOptions(),
        staleTime: 1000 * 60 * 5,
    });
}