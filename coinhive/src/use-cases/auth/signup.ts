import { authService } from "../../services/authService";
import { User } from "../../entities/User";

export const registerUser = async (name : string , email: string, password: string): Promise<User> => {
    return await authService.register(name,email, password);
}