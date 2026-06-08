import { useQuery } from "@tanstack/react-query";
import {
    accountService,
    type GetAccountByNameParams
} from "../../services/accountService";

export function useGetAccountByName({
    name,
}: GetAccountByNameParams) {
    return useQuery({
        queryKey: ["account-name", name],

        queryFn: () =>
            accountService.GetAccountByName({
                name,
            }),

        staleTime: 1000 * 60 * 5,
    });
}