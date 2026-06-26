import { useState, useMemo } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import Input from "../../components/Input/Input";
import Button from "../../components/Button/Button";
import styles from "./ResetPassword.module.css";
import { useAuth } from "../../hooks/Auth/Auth";
import {getStrength} from "./helpers";


const ResetPasswordPage = () => {
    const [searchParams] = useSearchParams();
    const token = searchParams.get("token");

    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState(false);
    const [done, setDone] = useState(false);

    const navigate = useNavigate();
    const {resetPassword} = useAuth();

    const strength = useMemo(() => getStrength(password), [password]);

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
        <div className={styles.globalContainer}>
            <div className={styles.card}>
                <h2 className={styles.brandName}>FinanceCore</h2>
                <p className={styles.titleP}>
                    Choose a strong password. It must be at least 8 characters long.
                </p>
                <div className={styles.inputField}>
                    <label htmlFor="reset-pw">New password</label>
                    <Input
                        id="reset-pw"
                        type="password"
                        placeholder="Enter new password"
                        value={password}
                        onChange={(e) => { setPassword(e.target.value); setError(null); }}
                    />
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
                {error && <p className={styles.error}>{error}</p>}
                <div className={styles.btns }>
                    <Button
                    type="submit"
                    onClick={handleSubmit}
                    disabled={loading}
                    fullwidth={true}
                    >
                    {loading ? "Updating…" : "Reset password"}
                    </Button> 
                    <Button variant="secondary" onClick={() => navigate("/login")}
                        fullwidth={true} >
                        Back to sign in
                    </Button>
                </div>
            </div>
        </div>
    );
};

export default ResetPasswordPage;