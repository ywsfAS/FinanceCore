import { useState, useMemo } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import Input from "../../components/Input/Input";
import Button from "../../components/Button/Button";
import styles from "./ResetPassword.module.css";
import { useTheme } from "../../hooks/Theme/Theme";
import { useAuth } from "../../hooks/Auth/Auth";

type Strength = { score: 0 | 1 | 2 | 3 | 4; label: string; color: string; width: string };

function getStrength(pw: string): Strength {
    if (!pw) return { score: 0, label: "", color: "#e5e7eb", width: "0%" };
    let score = 0;
    if (pw.length >= 8) score++;
    if (/[A-Z]/.test(pw)) score++;
    if (/[0-9]/.test(pw)) score++;
    if (/[^A-Za-z0-9]/.test(pw)) score++;

    const map: Record<number, Omit<Strength, "score">> = {
        1: { label: "Weak", color: "#ef4444", width: "25%" },
        2: { label: "Fair", color: "#f59e0b", width: "50%" },
        3: { label: "Good", color: "#3b82f6", width: "75%" },
        4: { label: "Strong", color: "#10b981", width: "100%" },
    };
    return { score: score as Strength["score"], ...(map[score] ?? map[1]) };
}

const ResetPasswordPage = () => {
    const [searchParams] = useSearchParams();
    const token = searchParams.get("token");

    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState(false);
    const [done, setDone] = useState(false);

    const { theme } = useTheme();
    const navigate = useNavigate();
    const {resetPassword} = useAuth();

    const strength = useMemo(() => getStrength(password), [password]);

    /* ── No / invalid token ── */
    if (!token) {
        return (
            <div className={`${styles.globalContainer} ${theme === "dark" ? styles.dark : ""}`}>
                <div className={styles.card}>
                    <div className={styles.brand}>
                        <div className={styles.brandIcon}>
                            <svg viewBox="0 0 20 20"><path d="M10 2L3 7v6l7 5 7-5V7l-7-5z" /></svg>
                        </div>
                        <span className={styles.brandName}>YourBrand</span>
                    </div>

                    <div className={styles.tokenError}>
                        <div className={styles.tokenErrorIcon}>
                            <svg viewBox="0 0 24 24">
                                <circle cx="12" cy="12" r="10" />
                                <line x1="12" y1="8" x2="12" y2="12" />
                                <line x1="12" y1="16" x2="12.01" y2="16" />
                            </svg>
                        </div>
                        <h1 className={styles.successTitle}>Invalid link</h1>
                        <p className={styles.successSub}>
                            This password reset link is missing or has expired.
                            Request a new one and try again.
                        </p>
                        <Button
                            type="button"
                            variant="primary"
                            className={styles.btnPrimary}
                            onClick={() => navigate("/forgot-password")}
                        >
                            Request new link
                        </Button>
                    </div>
                </div>
            </div>
        );
    }

    /* ── Success state ── */
    if (done) {
        return (
            <div className={`${styles.globalContainer} ${theme === "dark" ? styles.dark : ""}`}>
                <div className={styles.card}>
                    <div className={styles.brand}>
                        <div className={styles.brandIcon}>
                            <svg viewBox="0 0 20 20"><path d="M10 2L3 7v6l7 5 7-5V7l-7-5z" /></svg>
                        </div>
                        <span className={styles.brandName}>FinanceCore</span>
                    </div>

                    <div className={styles.successWrap}>
                        <div className={styles.successIcon}>
                            <svg viewBox="0 0 24 24">
                                <polyline points="20,6 9,17 4,12" />
                            </svg>
                        </div>
                        <h1 className={styles.successTitle}>Password updated!</h1>
                        <p className={styles.successSub}>
                            Your password has been reset successfully.
                            You can now sign in with your new password.
                        </p>
                        <Button
                            type="button"
                            variant="primary"
                            className={styles.btnPrimary}
                            onClick={() => navigate("/login")}
                        >
                            Go to sign in
                        </Button>
                    </div>
                </div>
            </div>
        );
    }

    /* ── Main form ── */
    const handleSubmit = async () => {
        setError(null);

        if (!password || password.trim().length === 0) {
            setError("Please enter a new password.");
            return;
        }
        if (password.length < 8) {
            setError("Password must be at least 8 characters.");
            return;
        }
        if (password !== confirmPassword) {
            setError("Passwords do not match.");
            return;
        }

        setLoading(true);
        try {
            await resetPassword(password, token);
            setDone(true);
        } catch {
            setError("Something went wrong. Please try again.");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className={`${styles.globalContainer} ${theme === "dark" ? styles.dark : ""}`}>
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

                {/* Lock icon */}
                <div className={styles.iconWrap}>
                    <svg viewBox="0 0 24 24">
                        <rect x="3" y="11" width="18" height="11" rx="2" ry="2" />
                        <path d="M7 11V7a5 5 0 0 1 10 0v4" />
                    </svg>
                </div>

                <h1 className={styles.title}>Set new password</h1>
                <p className={styles.titleP}>
                    Choose a strong password. It must be at least 8 characters long.
                </p>

                {/* New password */}
                <div className={styles.inputField}>
                    <label htmlFor="reset-pw">New password</label>
                    <Input
                        id="reset-pw"
                        type="password"
                        placeholder="Enter new password"
                        value={password}
                        onChange={(e) => { setPassword(e.target.value); setError(null); }}
                    />
                    {/* Strength meter */}
                    {password && (
                        <div className={styles.strengthWrap}>
                            <div className={styles.strengthBar}>
                                <div
                                    className={styles.strengthFill}
                                    style={{ width: strength.width, background: strength.color }}
                                />
                            </div>
                            <span className={styles.strengthLabel} style={{ color: strength.color }}>
                                {strength.label}
                            </span>
                        </div>
                    )}
                </div>

                {/* Confirm password */}
                <div className={styles.inputField}>
                    <label htmlFor="reset-confirm">Confirm password</label>
                    <Input
                        id="reset-confirm"
                        type="password"
                        placeholder="Confirm new password"
                        value={confirmPassword}
                        onChange={(e) => { setConfirmPassword(e.target.value); setError(null); }}
                    />
                </div>

                {/* Error */}
                {error && <p className={styles.error}>{error}</p>}

                {/* Submit */}
                <Button
                    type="submit"
                    variant="primary"
                    className={styles.btnPrimary}
                    onClick={handleSubmit}
                    disabled={loading}
                >
                    {loading ? "Updating…" : "Reset password"}
                </Button>

                {/* Back */}
                <p className={styles.backRow}>
                    <a className={styles.backLink} onClick={() => navigate("/login")}>
                        <svg viewBox="0 0 24 24">
                            <polyline points="15,18 9,12 15,6" />
                        </svg>
                        Back to sign in
                    </a>
                </p>
            </div>
        </div>
    );
};

export default ResetPasswordPage;