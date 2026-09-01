import type { AccountSummary, UserSummary } from '../entities/Report';
import { apiClient } from '../lib/apiClient';
export interface MonthlyAccountSummaryParams {
    accountId: string;
    year: number,
    month: number,
}
export interface MonthlyUserSpendingByCategoryParams {
    year: number,
    month: number,
}

export interface MonthlyAccountSpendingByCategoryParams {
    accountId: string,
    year: number,
    month: number,
}

export interface MonthlyUserSummaryParams {
    year: number,
    month: number,
}
export interface MonthlyUserTrendParams {
    month: number,
}

export const ReportService = {
    getUserSummary: () => {
        return apiClient<AccountSummary>(`/reports/monthly/accounts`);
    },
    getAccountMonthlySummary: ({ accountId, year, month }: MonthlyAccountSummaryParams) => {
        return apiClient(`/reports/monthly/accounts?id=${accountId}&year=${year}&month=${month}`);
    },
    getUserBySpendingCategoryMonthly: ({ year, month }: MonthlyUserSpendingByCategoryParams) => {
        return apiClient(`/reports/spending/by-category/user?year=${year}&month=${month}`);
    },
    getAccountSpendingCategoryMonthly: ({ accountId, year, month }: MonthlyAccountSpendingByCategoryParams) => {
        return apiClient(`/reports/spending/by-category/account?id=${accountId}&year=${year}&month=${month}`);
    },
    getUserNetWorth: () => {
        return apiClient(`/reports/net-worth`);
    },
    getUserMonthlySummary: ({ year, month }: MonthlyUserSummaryParams) => {
        return apiClient(`/reports/monthly/user?year=${year}&month=${month}`);
    },
    getUserMonthlyTrend: ({ month }: MonthlyUserTrendParams) => {
        return apiClient(`/reports/monthly/trend?lastNMonth=${month}`);
    }

}
