import { createContext } from "react";
import type { User } from "../../entities/User";
interface AuthContextType {
    user: User | null;
    isAuthenticated: boolean;
    loading: boolean;
    login: (token:string) => Promise<void>;
    loginWithToken: (token: string | null) => Promise<void>;
    logout: () => void;
    register: (name: string, email: string, password: string) => Promise<void>;
    loginWithCredentials: (email: string, password: string) => Promise<void>;
    forgetPassword: (email: string) => Promise<{ message: string }>;
    resetPassword: (newPassword: string, token: string) => Promise<{message : string}>

}
export const AuthContext = createContext<AuthContextType>({
    user: null,
    isAuthenticated: false,
    loading: true,
    login: async () => { },
    loginWithToken: async () => { },
    logout: () => { },
    register: async () => { },
    loginWithCredentials: async () => { },
    forgetPassword: async () => {
       return { message : "" }
    },
    resetPassword: async () => {
       return { message : "" }
    }
})