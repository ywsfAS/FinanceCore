import {useState} from "react";
import type { ReactNode } from "react";
import type { User } from "../entities/User";
import { AuthContext } from "./AuthContext";


type AuthProviderProps = {
    children: ReactNode;
};

export const AuthProvider = ({ children }: AuthProviderProps) => {
    const [user, setUser] = useState<User | null>(null);
    const [loading, setLoading] = useState<boolean>(true);


    const login = async (token: string) => {
        localStorage.setItem("token", token);
        await loginWithToken(token);

    }
    const logout = () => {
        localStorage.removeItem("token");
        setUser(null);
    }
    const loginWithToken = async (token : string) => {
        try {
            const res = await fetch("", {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            if (!res.ok) throw new Error("Invalid token");
            const userData: User = await res.json();
            setUser(userData);
        } catch {
            logout();
        } finally {
            setLoading(false);
        }
        const register = async (name : string , email : string , password : string) => {
            const token = await registerUser(name . email . password);
        }

}
    return (
        <AuthContext.Provider value= {{ user, isAuthenticated : !!user ,loading, login , loginWithToken , logout }}>
            { children }
        </AuthContext.Provider>
  );
};