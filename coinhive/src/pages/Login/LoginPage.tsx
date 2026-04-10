import { useState } from "react";
import Input from "../../components/Input/Input";
import Card from "../../components/Card/Card";
import SideImage from "../../components/SideImage/SideImage";
import Checkbox from "../../components/Checkbox/Checkbox";
import Button from "../../components/Button/Button";
import Image from "../../assets/side.png";
import Logo from "../../assets/Logo.png";
import styles from "./Login.module.css";
import { useAuth } from "../../hooks/Auth";
import { useTheme } from "../../hooks/Theme";
const LoginPage = () => {

    const messages = {
        success: "Welcome back",
        error: "Invalid email or password"
    }


    const { loginWithCredentials, user } = useAuth();
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [rememberMe, setRememberMe] = useState(false);
    const [status , setStatus] = useState<"error"|"success"|null>(null);
    const {theme} = useTheme();
    const handleLogin = async () => {
        if (!email || email.trim().length === 0) {
            alert("email can't be empty");
        }

        if (!password|| password.trim().length === 0) {
            alert("password can't be empty");
        }
        try {

            await loginWithCredentials(email, password);
            setStatus("success");
        }
        catch {
            setStatus("error");
        }
      }
    console.log(user);

    return (
        <div className={`${styles.globalContainer} ${theme === 'dark' && styles.dark} `}>
            <Card className="login-card" variant={ theme === 'dark' ? "darkMode" : ""}>
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
                <p className={styles.signUp}>Don't have an account? <a className={styles.signUpLink} > Sign Up</a> </p>
                    {status && <p className={status === "error" ? styles.error : styles.success}>{messages[status]}</p>}
            </div>
            <SideImage src={Image} alt="Login illustration" />
            </Card>
        </div>
    );
};
export default LoginPage;