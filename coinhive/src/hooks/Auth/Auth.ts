import { useContext } from "react"
import { AuthContext } from "../context/Auth/AuthContext"

export const useAuth = () => {
    return useContext(AuthContext);
}