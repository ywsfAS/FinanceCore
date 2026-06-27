import { useMutation } from "@tanstack/react-query";
import {
    SubscriptionService,
    type RemoveSubscriptionByIdParams
} from "../../services/subscriptionService";
export function useRemoveSubscription() {
    return useMutation({
        mutationFn: (subscription: RemoveSubscriptionByIdParams) => SubscriptionService.RemoveSubscriptionById(subscription),
    })
}