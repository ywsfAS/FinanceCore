import { useQuery } from "@tanstack/react-query";
import {
    ReportService,
    MonthlyUserSummaryParams,
} from "../../services/reportService";

export function useUserMonthlySummary({
    userId,
    year,
    month,
}: MonthlyUserSummaryParams) {
    return useQuery({
        queryKey: ["user-summary",userId, year, month],

        queryFn: () =>
            ReportService.getAccountMonthlySummary({
                userId,
                year,
                month,
            }),

        staleTime: 1000 * 60 * 5,
    });
}