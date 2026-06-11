import { useMutation } from "@tanstack/react-query";
import {
    budgetService,
    type RemoveBudgetParams
    } from "../../services/budgetService";
export function useRemoveBudget() {
    return useMutation({
        mutationFn: (budget: RemoveBudgetParams) => budgetService.RemoveBudget(budget),
    })
}