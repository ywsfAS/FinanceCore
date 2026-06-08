import { useMutation } from "@tanstack/react-query";
import {
    savingGoalService,
    type CancelGoalParams
} from "../../services/savingGoalService";
export function useCancelGoal() {
    return useMutation({
        mutationFn: (goalCancel: CancelGoalParams) => savingGoalService.CancelGoal(goalCancel),
    })
}