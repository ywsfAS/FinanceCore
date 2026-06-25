import { useState } from "react";
import { useNavigate } from "react-router-dom";
import Input from "../../components/Input/Input";
import Button from "../../components/Button/Button";
import styles from "./ForgotPassword.module.css";
import { useTheme } from "../../hooks/Theme/Theme";
import { useAuth } from "../../hooks/Auth/Auth";

const ForgotPassword = () => {
    const [email, setEmail] = useState("");
    const [status, setStatus] = useState<"error" | "success" | null>(null);
    const [loading, setLoading] = useState(false);
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
        <div className={styles.globalContainer}>
            <div className={styles.card}>
                <h2 className={styles.brandName}>FinanceCore</h2>
                        <p className={styles.titleP}>
                            Enter your email address and we'll send you a password reset link.
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
                        <div className={styles.btns}>
                            <Button
                            type="submit"
                            onClick={handleSubmit}
                            disabled={loading}
                            fullwidth={true}
                            >
                            {loading ? "Sending…" : "Send reset link"}
                            </Button>
                            <Button variant="secondary"  fullwidth={true} onClick={() => navigate("/login")}>
                                Back to sign in
                            </Button>
                        </div>
            </div>
        </div>
    );
};

export default ForgotPassword;