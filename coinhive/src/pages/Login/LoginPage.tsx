import { useState } from "react";
import Input from "../../components/Input/Input";
import Checkbox from "../../components/Checkbox/Checkbox";
import Button from "../../components/Button/Button";
import styles from "./Login.module.css";
import { useAuth } from "../../hooks/Auth/Auth";
import { useTheme } from "../../hooks/Theme/Theme";
import { Link } from "react-router-dom";

const LoginPage = () => {
    const messages = {
        success: "Welcome back 👋",
        error: "Invalid email or password",
    };

    const { loginWithCredentials, user } = useAuth();
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [rememberMe, setRememberMe] = useState(false);
    const [status, setStatus] = useState<"error" | "success" | null>(null);
    const { theme } = useTheme();

    const handleLogin = async () => {
        if (!email || email.trim().length === 0) {
            alert("Email can't be empty");
            return;
        }
        if (!password || password.trim().length === 0) {
            alert("Password can't be empty");
            return;
        }
        try {
            await loginWithCredentials(email, password);
            setStatus("success");
        } catch {
            setStatus("error");
        }
    };

    return (
        <div className={`${styles.globalContainer} ${theme === "dark" ? styles.dark : ""}`}>
            {/* Card */}
            <div className={styles.card}>

                {/* Brand */}
                <div className={styles.brand}>
                    <div className={styles.brandIcon}>
                        <svg viewBox="0 0 20 20">
                            <path d="M10 2L3 7v6l7 5 7-5V7l-7-5z" />
                        </svg>
                    </div>
                    <span className={styles.brandName}>FinanceCore</span>
                </div>

                {/* Heading */}
                <h1 className={styles.title}>Welcome back</h1>
                <p className={styles.titleP}>Sign in to continue to your workspace</p>

                {/* Email */}
                <div className={styles.inputField}>
                    <label htmlFor="login-email">Email address</label>
                    <Input
                        type="email"
                        placeholder="you@example.com"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                </div>

                {/* Password */}
                <div className={styles.inputField}>
                    <label htmlFor="login-password">Password</label>
                    <Input
                        type="password"
                        placeholder="Enter your password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />
                </div>

                {/* Remember me + Forgot */}
                <div className={styles.row}>
                    <Checkbox
                        label="Remember me"
                        checked={rememberMe}
                        onChange={(e) => setRememberMe(e.target.checked)}
                    />
                    <a className={styles.forgotPassword}>Forgot password?</a>
                </div>

                {/* Submit */}
                <Button
                    type="submit"
                    variant="primary"
                    className={styles.btnPrimary}
                    onClick={handleLogin}
                >
                    Sign in
                </Button>

                {/* Status */}
                {status && (
                    <p className={status === "error" ? styles.error : styles.success}>
                        {messages[status]}
                    </p>
                )}

                {/* Footer */}
                <p className={styles.signUp}>
                    Don't have an account?{" "}
                    <Link to="/register" className={styles.signUpLink} >
                        Sign up
                    </Link>
                </p>
            </div>
        </div>
    );
};

export default LoginPage;