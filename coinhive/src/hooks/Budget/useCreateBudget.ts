import { useMutation } from "@tanstack/react-query";
import {
    budgetService, 
    type CreateBudgetParams
} from "../../services/budgetService";
export function useCreateBudget() {
    return useMutation({
        mutationFn: (budget : CreateBudgetParams) => budgetService.CreateBudget(budget),
    })
}