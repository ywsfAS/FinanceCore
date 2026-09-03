import { useMutation } from "@tanstack/react-query";
import {
    transactionService,
    type ExportTransactionsParams
} from "../../services/transactionService";
export function useExportTransaction() {
    return useMutation({
        mutationFn: (transaction: ExportTransactionsParams) => transactionService.ExportTransactions(transaction)

    })
}