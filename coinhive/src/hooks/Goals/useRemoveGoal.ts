import { useMutation } from "@tanstack/react-query";
import {
    savingGoalService,
    type RemoveGoalParams
} from "../../services/savingGoalService";
export function useRemoveGoal() {
    return useMutation({
        mutationFn: (goalRmeove: RemoveGoalParams) => savingGoalService.RemoveGoal(goalRmeove),
    })
}