import { useEffect, useState } from "react";
import type { ReactNode } from "react";
import type { User } from "../../entities/User";
import { AuthContext } from "./AuthContext";
import { useForgotPassword, useLogin, useLogout, useLogoutAll, useRefreshToken, useRegister, useResetPassword } from "../../hooks/Auth/useAuthMutations";

type AuthProviderProps = { children: ReactNode };
let startupRefresh: Promise<void> | null = null;

export const AuthProvider = ({ children }: AuthProviderProps) => {
    const [user, setUser] = useState<User | null>(null);
    const registerMutation = useRegister();
    const loginMutation = useLogin();
    const forgotPasswordMutation = useForgotPassword();
    const resetPasswordMutation = useResetPassword();
    const refreshMutation = useRefreshToken();
    const logoutMutation = useLogout();
    const logoutAllMutation = useLogoutAll();

    const saveTokens = (token: string, refreshToken?: string) => {
        localStorage.setItem("token", token);
        if (refreshToken) localStorage.setItem("refreshToken", refreshToken);
    };

    useEffect(() => {
        const storedRefreshToken = localStorage.getItem("refreshToken");
        if (!storedRefreshToken) return;

        let cancelled = false;
        const refresh = async () => {
            if (startupRefresh) return startupRefresh;
            startupRefresh = (async () => {
                try {
                    const response = await refreshMutation.mutateAsync({ refreshToken: storedRefreshToken });
                    if (cancelled) return;
                    saveTokens(response.token, response.refreshToken);
                    setUser({ id: response.id, name: response.email, email: response.email });
                } catch {
                    if (cancelled) return;
                    localStorage.removeItem("token");
                    localStorage.removeItem("refreshToken");
                    setUser(null);
                }
            })().finally(() => {
                startupRefresh = null;
            });
            return startupRefresh;
        };

        void refresh();
        const refreshInterval = window.setInterval(refresh, 10 * 60 * 1000);
        return () => {
            cancelled = true;
            window.clearInterval(refreshInterval);
        };
    }, []);

    const loginWithCredentials = async (email: string, password: string) => {
        const response = await loginMutation.mutateAsync({ email, password });
        saveTokens(response.token, response.refreshToken);
        setUser({ id: response.id, name: response.email, email: response.email });
    };

    const register = async (name: string, email: string, password: string) => {
        await registerMutation.mutateAsync({ name, email, password });
    };

    const logout = async () => {
        const refreshToken = localStorage.getItem("refreshToken");
        if (refreshToken) await logoutMutation.mutateAsync({ refreshToken });
        localStorage.removeItem("token");
        localStorage.removeItem("refreshToken");
        setUser(null);
    };

    const refreshToken = async () => {
        const storedRefreshToken = localStorage.getItem("refreshToken");
        if (!storedRefreshToken) return;
        const response = await refreshMutation.mutateAsync({ refreshToken: storedRefreshToken });
        saveTokens(response.token, response.refreshToken);
    };

    const forgetPassword = async (email: string) => {
        await forgotPasswordMutation.mutateAsync(email);
        return { message: "Password reset instructions sent." };
    };

    const resetPassword = async (newPassword: string, token: string) => {
        await resetPasswordMutation.mutateAsync({ newPassword, token });
        return { message: "Password reset successfully." };
    };

    const logoutAll = async (userId: string) => {
        await logoutAllMutation.mutateAsync({ userId });
        localStorage.removeItem("token");
        localStorage.removeItem("refreshToken");
        setUser(null);
    };

    return (
        <AuthContext.Provider value={{
            user,
            isAuthenticated: Boolean(user),
            loading: loginMutation.isPending || registerMutation.isPending,
            login: async (token) => localStorage.setItem("token", token),
            logout,
            register,
            loginWithCredentials,
            forgetPassword,
            resetPassword,
            refreshToken,
            logoutAll,
        }}>
            {children}
        </AuthContext.Provider>
    );
};
