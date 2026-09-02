import { useMutation } from "@tanstack/react-query";
import { accountService, type AccountAlertParams } from "../../services/accountService";

export function useCreateAccountAlert() {
    return useMutation({
        mutationFn: (alert: AccountAlertParams) => accountService.CreateAccountAlert(alert),
    });
}