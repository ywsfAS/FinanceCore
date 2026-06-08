import { useMutation} from "@tanstack/react-query";
import {
    savingGoalService,
    type CreateGoalParams 
} from "../../services/savingGoalService";
export function useCreateGoal() {
    return useMutation({
        mutationFn: (goal : CreateGoalParams) => savingGoalService.CreateGoal(goal),
    })
}