import { useMutation } from "@tanstack/react-query";
import { accountService, type ReconcileAccountParams } from "../../services/accountService";

export function useReconcileAccount() {
    return useMutation({
        mutationFn: (reconciliation: ReconcileAccountParams) => accountService.ReconcileAccount(reconciliation),
    });
}