import { useQuery } from "@tanstack/react-query";
import {
    budgetService,
    type GetBudgetByIdParams 
} from "../../services/budgetService";

export function useGetBudgetById(budgetId: GetBudgetByIdParams) {
    return useQuery({
        queryKey: ["budget", budgetId],

        queryFn: () =>
            budgetService.getBudgetById(budgetId),

        staleTime: 1000 * 60 * 5,
        placeholderData: (prev) => prev,
    });
}