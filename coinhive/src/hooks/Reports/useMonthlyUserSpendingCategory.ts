import { useQuery } from "@tanstack/react-query";
import {
    ReportService,
    MonthlyUserSpendingByCategoryParams,
} from "../../services/reportService";

export function useUserSpendingByCategoryMonthly({
    year,
    month,
}: MonthlyUserSpendingByCategoryParams) {
    return useQuery({
        queryKey: ["user-spending-category", year, month],

        queryFn: () =>
            ReportService.getUserBySpendingCategoryMonthly({
                year,
                month,
            }),

        staleTime: 1000 * 60 * 5,
    });
}