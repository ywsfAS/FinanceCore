import { useQuery } from "@tanstack/react-query";
import { ReportService } from "../../services/reportService";

export function useUserNetWorth() {
    return useQuery({
        queryKey: ["user-net-worth"],
        queryFn: ReportService.getUserNetWorth,
        staleTime: 1000 * 60 * 5,
    });
}