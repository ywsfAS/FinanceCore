import { forwardRef, type InputHTMLAttributes } from "react";
import styles from "./Input.module.css";

type InputProps = {
    label?: string;
    variant?: "filled" | "outline";
    error?: boolean;
    fullWidth?: boolean;
} & InputHTMLAttributes<HTMLInputElement>;

const Input = forwardRef<HTMLInputElement, InputProps>(
    (
        {
            label,
            variant = "filled",
            error = false,
            fullWidth = false,
            disabled = false,
            className = "",
            ...props
        },
        ref
    ) => {
        const variantClass = styles[variant];
        const errorClass = error ? styles.error : "";
        const disabledClass = disabled ? styles.disabled : "";
        const fullWidthClass = fullWidth ? styles.fullWidth : "";

        const inputClass = `
            ${styles.input}
            ${variantClass}
            ${errorClass}
            ${disabledClass}
            ${fullWidthClass}
            ${className}
        `;

        return (
            <div className={styles.wrapper}>
                {label && <label className={styles.label}>{label}</label>}

                <input
                    ref={ref}
                    className={inputClass}
                    disabled={disabled}
                    {...props}
                />
            </div>
        );
    }
);

Input.displayName = "Input";

export default Input;