import { useMutation } from "@tanstack/react-query";
import {
    accountService,
    type RemoveAccountParams
} from "../../services/accountService";
export function useCreateAccount() {
    return useMutation({
        mutationFn: (account: RemoveAccountParams) => accountService.RemoveAccount(account),
    })
}