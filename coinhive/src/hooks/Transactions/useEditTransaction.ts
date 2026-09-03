import { useMutation } from "@tanstack/react-query";
import {
    transactionService,
    type EditTransactionParams
} from "../../services/transactionService";
export function useEditTransaction() {
    return useMutation({
        mutationFn: (transaction: EditTransactionParams) => transactionService.EditTransaction(transaction)

    })
}