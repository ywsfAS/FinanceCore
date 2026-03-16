import React, { createContext} from "react";
interface AuthContextType {
    user: User | null;
    setUser: (user: User | null) => void;
}
export const AuthContext = createContext<AuthContextType>({
    user: null,
    setUser: () => { }
})