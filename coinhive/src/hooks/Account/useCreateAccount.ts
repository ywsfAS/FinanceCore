import { useMutation } from "@tanstack/react-query";
import {
    accountService,
    type CreateAccountParams
} from "../../services/accountService";
export function useCreateAccount() {
    return useMutation({
        mutationFn: (account : CreateAccountParams) => accountService.CreateAccount(account),
    })
}