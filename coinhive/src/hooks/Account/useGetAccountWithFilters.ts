import { useQuery } from "@tanstack/react-query";
import {
    accountService,
    type GetAccountWithFiltersParams
} from "../../services/accountService";

export function useGetAccountsWithFilters(filters: GetAccountWithFiltersParams) {
    return useQuery({
        queryKey: ["accounts-user-filters", filters],

        queryFn: () =>
            accountService.getUserAccountsWithFilters(filters),

        staleTime: 1000 * 60 * 5,
        placeholderData: (prev) => prev,
    });
}