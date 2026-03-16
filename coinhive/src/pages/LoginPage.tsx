import { useState } from "react";
import { loginUser } from "../use-cases/auth/login";
import { useAuth } from "../hooks/Auth";

export const LoginPage = () => {
    const { setUser } = useAuth();

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const handleLogin = async () => {
        const user = await loginUser(email, password);
        setUser(user);
    };

    return (
        <div>
            <input
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                placeholder="Email"
            />

            <input
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                placeholder="Password"
                type="password"
            />

            <button onClick={handleLogin}>Login</button>
        </div>
    );
};