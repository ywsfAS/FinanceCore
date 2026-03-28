import type { ButtonHTMLAttributes, ReactNode } from "react";
import styles from "./Button.module.css";

// button props type 
type ButtonProps = {
    children: ReactNode;
    onClick?: () => void;
    variant?: 'primary' | 'secondary' | 'success' | 'warning';
    size?: 'small' | 'medium' | 'large';
    fullwidth?: boolean;
    disabled?: boolean;
} & ButtonHTMLAttributes<HTMLButtonElement>;
export default function Button({ children, onClick = () => {}, variant = 'primary', size = 'medium', fullwidth = false , disabled = false , ...props}: ButtonProps) {
    const fullwidthStyle = fullwidth ? styles.fullwidth : '';
    const disabledStyle = disabled ? styles.disabled : '';
    const style = `${styles.button} ${styles[variant]} ${styles[size]} ${fullwidthStyle} ${disabledStyle}`;
    console.log(style);
    return (
        <button className={style} onClick={onClick} {...props} >{children}</button>
    )
}