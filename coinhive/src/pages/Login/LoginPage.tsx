import {useState } from "react";
import Input from "../../components/Input/Input";
import Card from "../../components/Card/Card";
import SideImage from "../../components/SideImage/SideImage";
import Checkbox from "../../components/Checkbox/Checkbox";
import Button from "../../components/Button/Button";
import Image from "../../assets/image.jpeg";
import Logo from "../../assets/logo.svg";
import styles from "./Login.module.css";
import { useAuth } from "../../hooks/Auth";
const LoginPage = () => {

    const { loginWithCredentials , user } = useAuth();
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [rememberMe, setRememberMe] = useState(false);
    const handleLogin = async () => {
        if (!email || email.trim().length === 0) {
            alert("email can't be empty");
        }

        if (!password|| password.trim().length === 0) {
            alert("password can't be empty");
        }
        loginWithCredentials(email,password);
    }
    console.log(user);

    return (
    <div className={styles.globalContainer}>
        <Card className="login-card">
                <div className={styles.formSide}>
                    <div className={styles.logoContainer}>
                        <img className={styles.logo} src={Logo} alt="" />
                    </div>
                <h1 className={styles.title}>Welcome Back!</h1>
                <p className={styles.titleP}>Hey, Welcome back to your special place</p>
                 <div className={styles.input}>
                <p>Email</p>
                <Input type="email" placeholder="Enter your email" value={email} onChange={(e) => setEmail(e.target.value)} />
                </div>
                <div className ={styles.input}>
                <p>Password</p>
                <Input type="password" placeholder="Enter your password" value={password} onChange={(e) => setPassword(e.target.value)} />
                </div>
                <div className={styles.container}>
                  <Checkbox
                        label="Remember me"
                        checked={rememberMe}
                        onChange={(e) => setRememberMe(e.target.checked)}
                    />
                <a className={styles.forgotPassword}>Forgot password </a>
                </div>
                    <Button type="submit" onClick={handleLogin}>Login</Button>
                <p className={styles.signUp}>Don't have an account? <a className={styles.signUpLink}> Sign Up</a> </p>

            </div>
            <SideImage src={Image} alt="Login illustration" />
            </Card>
        </div>
    );
};
export default LoginPage;