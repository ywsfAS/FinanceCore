import { useQuery } from "@tanstack/react-query";
import {
    transactionService,
    type FiltredTransactionsParams
} from "../../services/transactionService";

export function useFiltredTransactions({
    CategoryId,Start,End,Page,PageSize,Type
}: FiltredTransactionsParams) {
    return useQuery({
        queryKey: ["filtred-transactions", CategoryId,Start,End,Page,PageSize,Type],

        queryFn: () =>
            transactionService.getFiltredTransactions({ CategoryId, Start, End, Page, PageSize, Type }),
        staleTime: 1000 * 60 * 5,
        placeholderData : (prev) => prev
    });
}