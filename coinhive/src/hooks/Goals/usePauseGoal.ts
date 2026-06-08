import { useMutation } from "@tanstack/react-query";
import {
    savingGoalService,
    type PauseGoalParams
} from "../../services/savingGoalService";
export function usePauseGoal() {
    return useMutation({
        mutationFn: (goalPause: PauseGoalParams) => savingGoalService.PauseGoal(goalPause),
    })
}