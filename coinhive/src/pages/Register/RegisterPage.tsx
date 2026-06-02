import { useState } from "react";
import { useAuth } from "../../hooks/Auth/Auth";
import Input from "../../components/Input/Input";
import Checkbox from "../../components/Checkbox/Checkbox";
import Button from "../../components/Button/Button";
import Logo from "../../assets/Logo.png";
import styles from "./Register.module.css";
import { useTheme } from "../../hooks/Theme/Theme";
import { Link } from "react-router-dom";

const RegisterPage = () => {
    const [name, setName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [agreeTerms, setAgreeTerms] = useState(false);
    const { register } = useAuth();
    const { theme } = useTheme();

    const handleRegister = async () => {
        if (password !== confirmPassword) {
            alert("Passwords do not match!");
            return;
        }
        if (!agreeTerms) {
            alert("You must agree to the terms and conditions.");
            return;
        }
        await register(name, email, password);
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
                <h1 className={styles.title}>Create account</h1>
                <p className={styles.titleP}>Join us and start managing your finances</p>

                {/* Name */}
                <div className={styles.inputField}>
                    <label htmlFor="reg-name">Full name</label>
                    <Input
                        type="text"
                        placeholder="Enter your full name"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                    />
                </div>

                {/* Email */}
                <div className={styles.inputField}>
                    <label htmlFor="reg-email">Email address</label>
                    <Input
                        type="email"
                        placeholder="you@example.com"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                </div>

                {/* Password */}
                <div className={styles.inputField}>
                    <label htmlFor="reg-password">Password</label>
                    <Input
                        type="password"
                        placeholder="Enter your password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />
                </div>

                {/* Confirm Password */}
                <div className={styles.inputField}>
                    <label htmlFor="reg-confirm">Confirm password</label>
                    <Input
                        type="password"
                        placeholder="Confirm your password"
                        value={confirmPassword}
                        onChange={(e) => setConfirmPassword(e.target.value)}
                    />
                </div>

                {/* Terms */}
                <div className={styles.container}>
                    <Checkbox
                        label="I agree to the terms and conditions"
                        checked={agreeTerms}
                        onChange={(e) => setAgreeTerms(e.target.checked)}
                    />
                </div>

                {/* Submit */}
                <Button
                    type="submit"
                    variant="primary"
                    className={styles.btnPrimary}
                    onClick={handleRegister}
                >
                    Create account
                </Button>

                {/* Footer */}
                <p className={styles.loginUp}>
                    Already have an account?{" "}
                    <Link to="/login" className={styles.loginUpLink}>
                        Sign in
                    </Link>
                </p>
            </div>
        </div>
    );
};

export default RegisterPage;