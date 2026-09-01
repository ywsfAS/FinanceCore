import { Link } from "react-router-dom";
import { useForm } from "react-hook-form";

import { useAuth } from "../../hooks/Auth/Auth";
import Input from "../../components/Input/Input";
import Checkbox from "../../components/Checkbox/Checkbox";
import Button from "../../components/Button/Button";

import styles from "./Register.module.css";
import Image from "../../assets/Auth.png";

export interface RegisterInfos {
    name: string;
    email: string;
    password: string;
    confirmPassword: string;
    agreeTerms: boolean;
}

const RegisterPage = () => {
    const { register: registerUser } = useAuth();

    const {
        register,
        handleSubmit,
        watch,
        formState: { errors },
    } = useForm<RegisterInfos>({
        defaultValues: {
            name: "",
            email: "",
            password: "",
            confirmPassword: "",
            agreeTerms: false,
        },
    });

    const password = watch("password");

    const onSubmit = async (data: RegisterInfos) => {
        console.log("Submitting...", data);
        await registerUser(
            data.name,
            data.email,
            data.password
        );
    };
    const onInvalid = (errors) => {
        console.log("Error", errors);
    }

    return (
        <div className={styles.page}>
            <div className={styles.container}>

                <div className={styles.imageContainer}>
                    <img src={Image} alt="Finance illustration" />
                </div>

                <form
                    className={styles.formContainer}
                    onSubmit={handleSubmit(onSubmit, onInvalid)}
                >
                    <h1 className={styles.title}>FinanceCore</h1>

                    <p className={styles.subtitle}>
                        Create an account to manage your money smarter.
                    </p>

                    <div className={styles.inputField}>
                        <label>Username</label>
                        <Input
                            type="text"
                            placeholder="Enter your username"
                            {...register("name", {
                                required: "Username is required",
                            })}
                        />
                    </div>

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

                    <div className={styles.inputField}>
                        <label>Confirm Password</label>
                        <Input
                            type="password"
                            placeholder="Confirm your password"
                            {...register("confirmPassword", {
                                validate: (value) =>
                                    value === password ||
                                    "Passwords do not match",
                            })}
                        />
                    </div>

                    <div className={styles.checkboxContainer}>
                        <Checkbox
                            label="I agree to the terms and conditions"
                            {...register("agreeTerms", {
                                required: true,
                            })}
                        />
                    </div>

                    <Button
                        type="submit"
                        variant="primary"
                    >
                        Create Account
                    </Button>

                    <p className={styles.loginText}>
                        Already have an account?{" "}
                        <Link
                            to="/login"
                            className={styles.loginLink}
                        >
                            Sign In
                        </Link>
                    </p>
                </form>

            </div>
        </div>
    );
};

export default RegisterPage;