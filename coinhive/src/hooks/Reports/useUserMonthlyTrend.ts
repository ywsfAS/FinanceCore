import { useQuery } from "@tanstack/react-query";
import  {
    ReportService,
    type MonthlyUserTrendParams
} from "../../services/reportService";

export function useUserMonthlyTrend({
    month = 4,
}: MonthlyUserTrendParams) {
    return useQuery({
        queryKey: ["user-trend", month],

        queryFn: () =>
            ReportService.getUserMonthlyTrend({
                month,
            }),

        staleTime: 1000 * 60 * 5,
    });
}