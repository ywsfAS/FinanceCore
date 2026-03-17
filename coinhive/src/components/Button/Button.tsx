import type { ButtonHTMLAttributes } from "react";
import "./Button.css";
interface ButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
    className? : string,
}
export default function Button({ children, className, ...props }: ButtonProps) {
    return (
        <button className={`custom-button ${className}`} {...props} >{children}</button>
    )
}