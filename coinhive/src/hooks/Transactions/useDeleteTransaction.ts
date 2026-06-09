import { useMutation } from "@tanstack/react-query";
import {
    transactionService,
    type DeleteTransactionParams
} from "../../services/transactionService";
export function useCreateAccount() {
    return useMutation({
        mutationFn: (transaction: DeleteTransactionParams) => transactionService.DeleteTransaction(transaction)

    })
}