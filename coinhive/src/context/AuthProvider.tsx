import {useState} from "react";
import type { ReactNode } from "react";
import type { User } from "../entities/User";
import { AuthContext } from "./AuthContext";


type AuthProviderProps = {
    children: ReactNode;
};
export const AuthProvider = ({ children }: AuthProviderProps) => {
    const [user, setUser] = useState<User | null>(null);

    return (
        <AuthContext.Provider value= {{ user, setUser }}>
            { children }
        </AuthContext.Provider>
  );
};