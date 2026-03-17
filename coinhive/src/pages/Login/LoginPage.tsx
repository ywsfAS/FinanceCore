import { useState } from "react";
import { loginUser } from "../../use-cases/auth/login";
import { useAuth } from "../../hooks/Auth";
import Input from "../../components/Input/Input";
import Card from "../../components/Card/Card";
import SideImage from "../../components/SideImage/SideImage";
import Checkbox from "../../components/Checkbox/Checkbox";
import Button from "../../components/Button/Button";
import Image from "../../assets/image.jpeg";
import Logo from "../../assets/logo.svg";
import "./Login.css";
export const LoginPage = () => {
    const { setUser } = useAuth();

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [rememberMe, setRememberMe] = useState(false);

    const handleLogin = async () => {
        const user = await loginUser(email, password);
        setUser(user);
    };

    return (
    <div className = "global-container">
        <Card className="login-card">
                <div className="form-side">
                    <div className="logo-container">
                        <img className="logo" src={Logo} alt="" />
                    </div>
                  <h1 className="title">Welcome Back!</h1>
                <p className="title-p">Hey, Welcome back to your special place</p>
                <div className="input">
                <p>Email</p>
                <Input type="email" placeholder="Enter your email" value={email} onChange={(e) => setEmail(e.target.value)} />
                </div>
                <div className = "input">
                <p>Password</p>
                <Input type="password" placeholder="Enter your password" value={password} onChange={(e) => setPassword(e.target.value)} />
                </div>
                <div className="container">
                  <Checkbox
                        label="Remember me"
                        checked={rememberMe}
                        onChange={(e) => setRememberMe(e.target.checked)}
                    />
                <a className="forgot-password">Forgot password </a>
                </div>
                <Button type="submit" onClick={handleLogin}>Login</Button>
                <p className="sign-up">Don't have an account? <a className="sign-up-link"> Sign Up</a> </p>

            </div>
            <SideImage src={Image} alt="Login illustration" />
            </Card>
        </div>
    );
};