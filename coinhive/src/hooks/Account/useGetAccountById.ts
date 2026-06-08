import { useQuery } from "@tanstack/react-query";
import {
    accountService,
    type GetAccountByIdParams
} from "../../services/accountService";

export function useGetAccountById({
   id,
}: GetAccountByIdParams) {
    return useQuery({
        queryKey: ["account-id", id],

        queryFn: () =>
            accountService.GetAccountById({
                id,
            }),

        staleTime: 1000 * 60 * 5,
    });
}