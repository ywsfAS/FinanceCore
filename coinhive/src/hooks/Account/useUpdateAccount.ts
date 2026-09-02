import { useMutation, useQueryClient } from "@tanstack/react-query";
import { accountService, type UpdateAccountParams } from "../../services/accountService";

export function useUpdateAccount() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: (account: UpdateAccountParams) => accountService.UpdateAccount(account),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["accounts-user-filters"] });
        },
    });
}