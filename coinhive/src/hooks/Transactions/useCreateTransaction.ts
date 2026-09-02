import { useMutation, useQueryClient } from "@tanstack/react-query";
import {
    transactionService,
    type CreateTransactionParams
} from "../../services/transactionService";
export function useCreateTransaction() {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: (transaction: CreateTransactionParams) => transactionService.CreateTransaction(transaction),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["filtred-transactions"] });
        },
    })
}