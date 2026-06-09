import { useMutation } from "@tanstack/react-query";
import {
    transactionService,
    type CreateTransactionParams 
} from "../../services/transactionService";
export function useCreateAccount() {
    return useMutation({
        mutationFn: (transaction : CreateTransactionParams) => transactionService.CreateTransaction(transaction)
    })
}