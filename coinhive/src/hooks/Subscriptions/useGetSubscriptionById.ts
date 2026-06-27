import { useQuery } from "@tanstack/react-query";
import {
    SubscriptionService,
    type GetSubscriptionByIdParams
} from "../../services/subscriptionService";

export function useGetSubscriptionById( param: GetSubscriptionByIdParams) {
    return useQuery({
        queryKey: ["Subscriptions-user-id", param],

        queryFn: () =>
            SubscriptionService.GetSubscriptionById(param),

        staleTime: 1000 * 60 * 5,
        placeholderData: (prev) => prev,
    });
}
