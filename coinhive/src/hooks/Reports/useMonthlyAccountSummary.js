import { useQuery } from "@tanstack/react-query";
import { ReportService } from "../../services/reportService";

interface Params {
    accountId?: string;
    year: number;
    month: number;
}

export function useMonthlyAccountSummary({
    accountId,
    year,
    month,
}: Params) {
    return useQuery({
        queryKey: ["monthly-account-summary", accountId, year, month],

        queryFn: () =>
            ReportService.getMonthlyAccountSummary({
                accountId: accountId!,
                year,
                month,
            }),

        enabled: !!accountId,
    });
}