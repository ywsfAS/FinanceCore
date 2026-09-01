import type { User } from "../entities/User";
import { apiClient } from "../lib/apiClient";

export const authService = {

    login: async (email: string, password: string): Promise<User> => {
        return await apiClient<User>(`/auth/login`, {
            method: "POST",
            headers: { "content-type": "application/json" },
            body: JSON.stringify({ email, password }),
        });
    },
    forgetPassword: async (email: string): Promise<{ message: string }> => {
        return await apiClient(`/auth/forgot-password`, {
            method: "POST",
            headers: { "content-type": "application/json" },
            body: JSON.stringify({ email: { address: email } }),
        });

    },
    resetPassword: async (newPassword: string, token: string): Promise<{ message: string }> => {
        return await apiClient(`/auth/reset-password`, {
            method: "POST",
            headers: { "content-type": "application/json" },
            body: JSON.stringify({ token, newPassword }),
        });
    },
    register: async (name: string, email: string, password: string): Promise<User> => {
        return await apiClient(`/auth/register`, {
            method: "POST",
            headers: { "Content-type": "application/json" },
            body: JSON.stringify({ name, email, password }),
        });
    },
}