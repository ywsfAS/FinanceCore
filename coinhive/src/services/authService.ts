import type { User } from "../entities/User";

const auth_URL = "/api/auth";

export const authService = {

    login: async (email : string , password : string) : Promise<User > => {
        const res = await fetch(`${auth_URL}/login`, {
            method: "POST",
            headers: { "content-type": "application/json" },
            body: JSON.stringify({email,password}),
        });
        if (!res.ok) throw new Error("Login failed");
        return res.json();
    },

    register: async (name: string, email: string, password: string): Promise<User> => {
        const res = await fetch(`${auth_URL}/register`, {
            method: "POST",
            headers: { "conent-type": "aplication/json" },
            body: JSON.stringify({name,email,password}),
        });
        if (!res.ok) throw new Error("Register failed");
        return res.json();

    }
}