import { useState } from "react";
import type { ReactNode } from "react";
import type { User } from "../../entities/User";
import { AuthContext } from "./AuthContext";
import { registerUser } from "../../use-cases/auth/signup"
import { loginUser } from "../../use-cases/auth/login";
import { authService } from "../../services/authService";


type AuthProviderProps = {
    children: ReactNode;
};
const { forgetPassword, resetPassword } = authService;
export const AuthProvider = ({ children }: AuthProviderProps) => {
    const [user, setUser] = useState<User | null>(null);
    const [loading, setLoading] = useState<boolean>(true);

    const saveJwtToken = async (token: string) => {
        console.log(token);
        localStorage.setItem("token", token);
    };

    const RemoveJwtToken = () => {
        localStorage.removeItem("token");
        setUser(null);
    };

    const loginWithCredentials = async (email: string, password: string) => {
        const { token } = await loginUser(email, password);
        saveJwtToken(token!);

    }
    const register = async (name: string, email: string, password: string) => {
        const { token } = await registerUser(name, email, password);
        saveJwtToken(token!);
    };

    return (
        <AuthContext.Provider
            value={{
                user,
                isAuthenticated: !!user,
                loading,
                login: saveJwtToken,
                logout: RemoveJwtToken,
                register,
                loginWithCredentials,
                forgetPassword,
                resetPassword

            }}
        >
            {children}
        </AuthContext.Provider>
    );
};