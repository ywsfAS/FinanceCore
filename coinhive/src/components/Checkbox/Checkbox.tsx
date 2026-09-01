import "./Checkbox.css";
import type { InputHTMLAttributes } from "react";

interface CheckboxProps extends InputHTMLAttributes<HTMLInputElement> {
    label: string;
    className?: string;
}

export default function Checkbox({
    label,
    className = "",
    ...props
}: CheckboxProps) {
    return (
        <label className={`checkbox-container ${className}`}>
            <input
                type="checkbox"
                {...props}
            />
            <span className="checkmark"></span>
            {label}
        </label>
    );
}