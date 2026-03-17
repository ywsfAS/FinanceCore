import type { ChangeEvent } from "react";
import "./Checkbox.css";

interface CheckboxProps {
    label: string;
    checked: boolean;
    onChange: (e: ChangeEvent<HTMLInputElement>) => void;
    className?: string;
}

export default function Checkbox({
    label,
    checked,
    onChange,
    className = ""
}: CheckboxProps) {
    return (
        <label className={`checkbox-container ${className}`}>
            <input
                type="checkbox"
                checked={checked}
                onChange={onChange}
            />
            <span className="checkmark"></span>
            {label}
        </label>
    );
}