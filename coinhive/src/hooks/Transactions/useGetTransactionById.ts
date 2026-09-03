import { useMutation } from "@tanstack/react-query";
import {
    transactionService,
    type GetTransactionByIdParams
} from "../../services/transactionService";
export function useGetTransactionById() {
    return useMutation({
        mutationFn: (transaction: GetTransactionByIdParams) => transactionService.GetTransactionById(transaction)

    })
}