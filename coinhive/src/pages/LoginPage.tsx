import { useState } from "react";
import { loginUser } from "../use-cases/auth/login";
import { useAuth } from "../hooks/Auth";
import Input from "../components/Input/Input";
import Card from "../components/Card/Card";
import SideImage from "../components/SideImage/SideImage";
import Button from "../components/Button/Button";
import Image from "../assets/Login.png";
export const LoginPage = () => {
    const { setUser } = useAuth();

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const handleLogin = async () => {
        const user = await loginUser(email, password);
        setUser(user);
    };

    return (
        <Card className="login-card">
            <div className="form-side">
                <h2>Welcome Back!</h2>
                <Input type="email" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} />
                <Input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} />
                <Button type="submit" onClick={handleLogin}>Login</Button>
            </div>
            <SideImage src={Image} alt="Login illustration" />
        </Card>
    );
};