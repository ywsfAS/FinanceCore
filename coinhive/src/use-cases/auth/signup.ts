import { authService } from "../../services/authService";
import type { User } from "../../entities/User";

type AuthResponse = {
    user: User;
    token: string;
};

export const registerUser = async (
    name: string,
    email: string,
    password: string
): Promise<AuthResponse> => {
    return await authService.register(name, email, password);
};