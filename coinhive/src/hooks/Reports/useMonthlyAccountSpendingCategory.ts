import { useQuery } from "@tanstack/react-query";
import {
    ReportService,
    MonthlyAccountSpendingByCategoryParams,
} from "../../services/reportService";

interface Params extends MonthlyAccountSpendingByCategoryParams {
    accountId?: string;
}

export function useAccountSpendingCategoryMonthly({
    accountId,
    year,
    month,
}: Params) {
    return useQuery({
        queryKey: [
            "account-spending-category",
            accountId,
            year,
            month,
        ],

        queryFn: () =>
            ReportService.getAccountSpendingCategoryMonthly({
                accountId: accountId!,
                year,
                month,
            }),

        enabled: !!accountId,

        staleTime: 1000 * 60 * 5,
    });
}