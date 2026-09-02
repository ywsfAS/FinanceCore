import { useMutation, useQueryClient } from "@tanstack/react-query";
import {
    accountService,
    type CreateAccountParams
} from "../../services/accountService";
export function useCreateAccount() {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (account: CreateAccountParams) => accountService.CreateAccount(account),
        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: ["accounts-user-filters"]
            })
        }
    })
}