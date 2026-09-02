import { useMutation, useQuery } from "@tanstack/react-query";
import { authService, type LoginHistoryParams, type LoginParams, type LogoutAllParams, type LogoutParams, type RefreshTokenParams, type RegisterParams, type ResetPasswordParams } from "../../services/authService";

export const useRegister = () => useMutation({ mutationFn: (params: RegisterParams) => authService.register(params) });
export const useLogin = () => useMutation({ mutationFn: (params: LoginParams) => authService.login(params) });
export const useForgotPassword = () => useMutation({ mutationFn: (email: string) => authService.forgotPassword(email) });
export const useResetPassword = () => useMutation({ mutationFn: (params: ResetPasswordParams) => authService.resetPassword(params) });
export const useRefreshToken = () => useMutation({ mutationFn: (params: RefreshTokenParams) => authService.refresh(params) });
export const useLogout = () => useMutation({ mutationFn: (params: LogoutParams) => authService.logout(params) });
export const useLogoutAll = () => useMutation({ mutationFn: (params: LogoutAllParams) => authService.logoutAll(params) });
export const useLoginHistory = (filters: LoginHistoryParams = {}) => useQuery({ queryKey: ["login-history", filters], queryFn: () => authService.loginHistory(filters), enabled: Boolean(localStorage.getItem("token")) });
