import { useQuery } from "@tanstack/react-query";
import {
    budgetService,
    type GetBudgetsWithFiltersParams
} from "../../services/budgetService";

export function useGetBudgetsWithFilters(filters: GetBudgetsWithFiltersParams) {
    return useQuery({
        queryKey: ["budgets-user-filters", filters],

        queryFn: () =>
            budgetService.getBudgetsWithFilters(filters),

        staleTime: 1000 * 60 * 5,
        placeholderData: (prev) => prev,
    });
}