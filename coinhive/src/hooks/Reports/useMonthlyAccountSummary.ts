import { useQuery } from "@tanstack/react-query";
import {
    ReportService,
    type MonthlyAccountSummaryParams,
} from "../../services/reportService";

export function useAccountMonthlySummary({
    accountId,
    year,
    month,
}: MonthlyAccountSummaryParams) {
    return useQuery({
        queryKey: ["account-summary",accountId,year, month],

        queryFn: () =>
            ReportService.getAccountMonthlySummary({
                accountId,
                year,
                month,
            }),

        staleTime: 1000 * 60 * 5,
    });
}