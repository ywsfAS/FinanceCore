import { useState } from "react";
import { useAuth } from "../../hooks/Auth";
import Input from "../../components/Input/Input";
import Card from "../../components/Card/Card";
import SideImage from "../../components/SideImage/SideImage";
import Checkbox from "../../components/Checkbox/Checkbox";
import Button from "../../components/Button/Button";
import Image from "../../assets/image.jpeg";
import Logo from "../../assets/logo.svg";
import styles from  "./Register.module.css";
import { useTheme } from "../../hooks/Theme";

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
        <div className={`${styles.globalContainer} ${theme === 'dark' && styles.dark }`}>
            <Card className={styles.loginCard} variant={theme === 'dark' ? "darkMode" : ""}>
                <div className={styles.formSide}>
                    <div className={styles.logoContainer}>
                        <img className={styles.logo} src={Logo} alt="Logo" />
                    </div>
                    <h1 className={styles.title}>Create Account</h1>
                    <p className={styles.titleP}>Join us and start managing your finances</p>

                    <div className={styles.input}>
                        <p>Name</p>
                        <Input type="text" placeholder="Enter your full name" value={name} onChange={(e) => setName(e.target.value)} />
                    </div>
                    <div className={styles.input}>

                        <p>Email</p>
                        <Input type="email" placeholder="Enter your email" value={email} onChange={(e) => setEmail(e.target.value)} />
                    </div>
                    <div className={styles.input}>
                        <p>Password</p>
                        <Input type="password" placeholder="Enter your password" value={password} onChange={(e) => setPassword(e.target.value)} />
                    </div>
                    <div className={styles.input}>
                        <p>Confirm Password</p>
                        <Input type="password" placeholder="Confirm your password" value={confirmPassword} onChange={(e) => setConfirmPassword(e.target.value)} />
                    </div>

                    <div className={styles.container}>
                        <Checkbox
                            label="I agree to the terms and conditions"
                            checked={agreeTerms}
                            onChange={(e) => setAgreeTerms(e.target.checked)}
                        />
                    </div>

                    <Button type="submit" onClick={handleRegister} variant='primary'>Register</Button>

                    <p className={styles.loginUp}>
                        Already have an account? <a className={styles.loginUpLink}>Login</a>
                    </p>
                </div>
                <SideImage src={Image} alt="Register illustration" />
            </Card>
        </div>
    );
};
export default RegisterPage;