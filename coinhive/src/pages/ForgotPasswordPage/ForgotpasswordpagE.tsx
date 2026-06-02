import { useState } from "react";
import { useNavigate } from "react-router-dom";
import Input from "../../components/Input/Input";
import Button from "../../components/Button/Button";
import styles from "./ForgotPassword.module.css";
import { useTheme } from "../../hooks/Theme/Theme";
import { useAuth } from "../../hooks/Auth/Auth";

const ForgotPasswordPage = () => {
    const [email, setEmail] = useState("");
    const [status, setStatus] = useState<"error" | "success" | null>(null);
    const [loading, setLoading] = useState(false);
    const { theme } = useTheme();
    const navigate = useNavigate();
    const {forgetPassword} = useAuth();

    const handleSubmit = async () => {
        if (!email || email.trim().length === 0) {
            setStatus("error");
            return;
        }

        setLoading(true);
        try {
            await forgetPassword(email);
           setStatus("success");
        } catch {
            setStatus("error");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className={`${styles.globalContainer} ${theme === "dark" ? styles.dark : ""}`}>
            <div className={styles.card}>

                <div className={styles.brand}>
                    <div className={styles.brandIcon}>
                        <svg viewBox="0 0 20 20">
                            <path d="M10 2L3 7v6l7 5 7-5V7l-7-5z" />
                        </svg>
                    </div>
                    <span className={styles.brandName}>FinanceCore</span>
                </div>

                {status === "success" ? (
                    <div className={styles.sentWrap}>
                        <div className={styles.sentIcon}>
                            <svg viewBox="0 0 24 24">
                                <path d="M4 4h16v16H4z" stroke="none" />
                                <polyline points="22,6 12,13 2,6" />
                                <rect x="2" y="4" width="20" height="16" rx="2" />
                                <polyline points="6,12 10,16 18,8" />
                            </svg>
                        </div>
                        <h1 className={styles.sentTitle}>Check your email</h1>
                        <p className={styles.sentSub}>
                            We sent a password reset link to{" "}
                            <span className={styles.sentEmail}>{email}</span>.
                            Check your inbox and follow the instructions.
                        </p>
                        <Button
                            type="button"
                            variant="primary"
                            className={styles.btnPrimary}
                            onClick={() => navigate("/login")}
                        >
                            Back to sign in
                        </Button>
                        <p className={styles.backRow}>
                            Didn't receive it?{" "}
                            <a
                                className={styles.backLink}
                                onClick={() => { setStatus(null); setEmail(""); }}
                            >
                                Try again
                            </a>
                        </p>
                    </div>
                ) : (
                    <>
                        <div className={styles.iconWrap}>
                            <svg viewBox="0 0 24 24">
                                <rect x="2" y="4" width="20" height="16" rx="2" />
                                <polyline points="22,6 12,13 2,6" />
                            </svg>
                        </div>

                        <h1 className={styles.title}>Forgot password?</h1>
                        <p className={styles.titleP}>
                            No worries — enter your email and we'll send you a reset link right away.
                        </p>

                        <div className={styles.inputField}>
                            <label htmlFor="forgot-email">Email address</label>
                            <Input
                                type="email"
                                placeholder="you@example.com"
                                value={email}
                                onChange={(e) => { setEmail(e.target.value); setStatus(null); }}
                            />
                        </div>

                        {status === "error" && (
                            <p className={styles.error}>
                                Please enter a valid email address.
                            </p>
                        )}

                        <Button
                            type="submit"
                            variant="primary"
                            className={styles.btnPrimary}
                            onClick={handleSubmit}
                            disabled={loading}
                        >
                            {loading ? "Sending…" : "Send reset link"}
                        </Button>

                        <p className={styles.backRow}>
                            <a className={styles.backLink} onClick={() => navigate("/login")}>
                                <svg viewBox="0 0 24 24">
                                    <polyline points="15,18 9,12 15,6" />
                                </svg>
                                Back to sign in
                            </a>
                        </p>
                    </>
                )}
            </div>
        </div>
    );
};

export default ForgotPasswordPage;