import {useState} from "react";
import type { ReactNode } from "react";
import type { User } from "../../entities/User";
import { AuthContext } from "./AuthContext";
import { registerUser} from "../../use-cases/auth/signup"
import { loginUser } from "../../use-cases/auth/login";


type AuthProviderProps = {
    children: ReactNode;
};

export const AuthProvider = ({ children }: AuthProviderProps) => {
    const [user, setUser] = useState<User | null>(null);
    const [loading, setLoading] = useState<boolean>(true);

    const login = async (token: string) => {
        console.log(token);
        localStorage.setItem("token", token);
        await loginWithToken(token);
    };

    const logout = () => {
        localStorage.removeItem("token");
        setUser(null);
    };

    const loginWithToken = async (token: string | null) => {
        try {
            const res = await fetch("https://localhost:7143/api/v1/users", {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

            if (!res.ok) throw new Error("Invalid token");

            const userData: User = await res.json();
            if (token) userData.token = token;
            setUser(userData);
        } catch {
            logout();
        } finally {
            setLoading(false);
        }
    };
    const loginWithCredentials = async (email : string , password : string) => {
        const { token} = await loginUser(email, password);
        await login(token!);

    }
    const register = async (name: string, email: string, password: string) => {
        const {token} = await registerUser(name, email, password);
        await login(token!);
    };

    return (
        <AuthContext.Provider
            value={{
                user,
                isAuthenticated: !!user,
                loading,
                login,
                loginWithToken,
                logout,
                register,
                loginWithCredentials,
            }}
        >
            {children}
        </AuthContext.Provider>
    );
};