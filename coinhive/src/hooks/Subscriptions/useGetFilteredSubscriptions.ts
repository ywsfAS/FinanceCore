import { useQuery } from "@tanstack/react-query";
import {
    SubscriptionService,
    type GetFilteredSubscriptionsParams
} from "../../services/subscriptionService";

export function useGetFilteredSubscriptions(filters: GetFilteredSubscriptionsParams) {
    return useQuery({
        queryKey: ["Subscriptions-user", filters],

        queryFn: () =>
            SubscriptionService.GetFilteredSubscription(filters),

        staleTime: 1000 * 60 * 5,
        placeholderData: (prev) => prev,
    });
}