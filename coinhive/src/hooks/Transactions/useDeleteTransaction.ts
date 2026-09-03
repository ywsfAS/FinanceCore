import { useMutation } from "@tanstack/react-query";
import {
    transactionService,
    type DeleteTransactionParams
} from "../../services/transactionService";
export function useRemoveTransaction() {
    return useMutation({
        mutationFn: (transaction: DeleteTransactionParams) => transactionService.DeleteTransaction(transaction)

    })
}