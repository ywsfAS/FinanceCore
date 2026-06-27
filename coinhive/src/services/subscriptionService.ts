import { apiClient } from '../lib/apiClient';


export type Period = 'Daily' | 'Weekly' | 'Monthly' | 'Quarterly' | 'Yearly';
export interface GetFilteredSubscriptionsParams {
    accountId?: string;
    categoryId?: string;
    isActive?: Boolean;
    period?: Period;
    start?: Date;
    end?: Date;
    page: number;
    pageSize: number;
}

export interface GetSubscriptionByIdParams {
    id: string;
}

export interface RemoveSubscriptionByIdParams {
    id: string;
}
export interface CreateSubscription {
    accountId: string;
    categoryId: string;
    amount: number;
    period: Period;
    description?: string;
    startDate: Date;
    endDate: Date;
}
 class SubscriptionRepository {
    
    private basePath = '/recurring-transactions';


    public async GetFilteredSubscriptions({ accountId,categoryId,isActive,period,start,end,page = 1,pageSize = 10} : GetFilteredSubscriptionsParams) {
        const params = new URLSearchParams();

        params.append("Page", page);
        params.append("PageSize", pageSize);
        if (accountId) params.append("AccountId", accountId);
        if (categoryId) params.append("CategoryId", categoryId);
        if (isActive) params.append("IsActive", isActive);
        if (period) params.append("Period", period);
        if (start) params.append("Start", start);
        if (end) params.append("End", end);

        return apiClient(`${this.basePath}?${params}`);
    }
    public async GetSubscriptionById({ id }: GetSubscriptionByIdParams ) {

        return apiClient(`${this.basePath}/${id}`);
    }

    public async RemoveSubscriptionById({ id }: RemoveSubscriptionByIdParams ) {

        return apiClient(`${this.basePath}/${id}`, {
            method : 'DELETE'
        });
    }
    public async CreateSubscription(subscription  : CreateSubscription) {

        return apiClient(`${this.basePath}`, {
            method: 'POST',
            body : JSON.stringify(subscription)
        });
    }
    }

}
export const SubscriptionService = new SubscriptionRepository();
