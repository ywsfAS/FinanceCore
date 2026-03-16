import { authService } from "../../services/authService";
import type { User } from "../../entities/User";

export const loginUser = async (email: string, password: string): Promise<User> => {
    return await authService.login(email,password);
}