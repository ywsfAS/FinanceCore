import { useMutation } from "@tanstack/react-query";
import {
    transactionService,
    type ImportTransactionsParams
} from "../../services/transactionService";
export function useImportTransaction() {
    return useMutation({
        mutationFn: (transaction: ImportTransactionsParams) => transactionService.ImportTransactions(transaction)

    })
}