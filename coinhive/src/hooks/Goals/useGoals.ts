import { useQuery } from "@tanstack/react-query";
import {
    savingGoalService,
    type GetGoalsParams
} from "../../services/savingGoalService";

export function useGoals({
    page = 1,
    pageSize = 5,
}: GetGoalsParams) {
    return useQuery({
        queryKey: ["user-goals", page, pageSize],
        queryFn: () =>
            savingGoalService.GetGoals({
                page,
                pageSize,
            }),
        staleTime: 1000 * 60 * 5,
    });
}