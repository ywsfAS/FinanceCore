import { Link } from "react-router-dom";
import { useForm } from "react-hook-form";

import { useAuth } from "../../hooks/Auth/Auth";
import Input from "../../components/Input/Input";
import Checkbox from "../../components/Checkbox/Checkbox";
import Button from "../../components/Button/Button";

import styles from "./Login.module.css";
import Image from "../../assets/Auth.png";

export interface LoginInfos {
    email: string;
    password: string;
    rememberMe: boolean;
}

const LoginPage = () => {
    const { loginWithCredentials } = useAuth();

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<LoginInfos>({
        defaultValues: {
            email: "",
            password: "",
            rememberMe: false,
        },
    });

    const onSubmit = async (data: LoginInfos) => {
        const {email,password} = data;
        await loginWithCredentials(email,password);
    };

    return (
        <div className={styles.page}>
            <div className={styles.container}>

                <div className={styles.imageContainer}>
                    <img src={Image} alt="Finance illustration" />
                </div>

                <form
                    className={styles.formContainer}
                    onSubmit={handleSubmit(onSubmit)}
                >
                    <h1 className={styles.title}>FinanceCore</h1>

                    <p className={styles.subtitle}>
                        Welcome back. Sign in to continue managing your finances.
                    </p>

                    <div className={styles.inputField}>
                        <label>Email</label>
                        <Input
                            type="email"
                            placeholder="Enter your email"
                            {...register("email", {
                                required: "Email is required",
                            })}
                        />
                    </div>

                    <div className={styles.inputField}>
                        <label>Password</label>
                        <Input
                            type="password"
                            placeholder="Enter your password"
                            {...register("password", {
                                required: "Password is required",
                            })}
                        />
                    </div>

                    <div className={styles.optionsRow}>
                        <Checkbox
                            label="Remember me"
                            {...register("rememberMe")}
                        />

                        <Link to="/forgot-password" className={styles.forgotLink}>
                            Forgot password?
                        </Link>
                    </div>

                    <Button type="submit" variant="primary">
                        Sign In
                    </Button>

                    <p className={styles.registerText}>
                        Don't have an account?{" "}
                        <Link to="/register" className={styles.registerLink}>
                            Create Account
                        </Link>
                    </p>
                </form>

            </div>
        </div>
    );
};

export default LoginPage;