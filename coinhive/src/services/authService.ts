import { apiClient } from "../lib/apiClient";

export interface RegisterParams { name: string; email: string; password: string; }
export interface LoginParams { email: string; password: string; }
export interface LoginResponse { id: string; email: string; token: string; refreshToken: string; tokenExpiresAt: string; }
export interface RegisterResponse { id: string; name: string; email: string; }
export interface RefreshTokenParams { refreshToken: string; }
export interface LogoutParams { refreshToken: string; }
export interface LogoutAllParams { userId: string; }
export interface ResetPasswordParams { token: string; newPassword: string; }
export interface LoginHistoryParams {
    status?: "Success" | "Failed" | "LockedOut";
    search?: string;
    from?: string;
    to?: string;
    page?: number;
    pageSize?: number;
}
export interface LoginHistoryItem {
    id: string;
    loginAt: string;
    ipAddress: string;
    userAgent: string;
    deviceName: string;
    os: string;
    status: "Success" | "Failed" | "LockedOut";
    failureReason: string;
}
export interface LoginHistoryResponse {
    items: LoginHistoryItem[];
    totalCount: number;
    page: number;
    pageSize: number;
}

export const authService = {
    register: (params: RegisterParams) => apiClient<RegisterResponse>("/auth/register", { method: "POST", body: JSON.stringify(params) }),
    login: (params: LoginParams) => apiClient<LoginResponse>("/auth/login", { method: "POST", body: JSON.stringify(params) }),
    forgotPassword: (email: string) => apiClient<void>("/auth/forgot-password", { method: "POST", body: JSON.stringify({ email }) }),
    resetPassword: (params: ResetPasswordParams) => apiClient<void>("/auth/reset-password", { method: "POST", body: JSON.stringify(params) }),
    refresh: (params: RefreshTokenParams) => apiClient<LoginResponse>("/auth/refresh", { method: "POST", body: JSON.stringify(params) }),
    logout: (params: LogoutParams) => apiClient<void>("/auth/logout", { method: "POST", body: JSON.stringify(params) }),
    logoutAll: (params: LogoutAllParams) => apiClient<void>("/auth/logout-all", { method: "POST", body: JSON.stringify(params) }),
    loginHistory: (filters: LoginHistoryParams = {}) => {
        const query = new URLSearchParams();
        Object.entries(filters).forEach(([key, value]) => { if (value !== undefined && value !== "") query.set(key, String(value)); });
        return apiClient<LoginHistoryResponse>(`/auth/login-history?${query.toString()}`);
    },
};
