import { useQuery } from "@tanstack/react-query";
import {
    savingGoalService,
    type GetGoalsParams 
} from "../../services/savingGoalService";

export function useGoals{
    page = 1,
    pageSize = 5
}: GetGoalsParams) {
    return useQuery({
        queryKey: ["user-goals", month],

        queryFn: () =>
            savingGoalService.GetGoals({
                page,PageSize
            }),

        staleTime: 1000 * 60 * 5,
    });
}