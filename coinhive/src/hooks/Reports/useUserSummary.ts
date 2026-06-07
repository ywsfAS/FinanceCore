import { useQuery } from "@tanstack/react-query";
import { ReportService } from "../../services/reportService";

export function useUserSummary() {
    return useQuery({
        queryKey: ["user-summary"],
        queryFn: () =>
            ReportService.getUserSummary(),
    });
}