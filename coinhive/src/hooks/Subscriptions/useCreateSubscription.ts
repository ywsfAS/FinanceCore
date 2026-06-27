import { useMutation } from "@tanstack/react-query";
import {
    SubscriptionService,
    type CreateSubscription 
} from "../../services/subscriptionService";
export function useCreateSubscription() {
    return useMutation({
        mutationFn: (subscription : CreateSubscription) => SubscriptionService.CreateSubscription(subscription), 
    })
}