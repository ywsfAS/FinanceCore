import React, { ChangeEvent } from "react";
import "./Input.css";

interface InputProps {
    type: string;
    placeholder?: string;
    value: string;
    onChange: (e: ChangeEvent<HTMLInputElement>) => void;
    className?: string;
}

export default function Input({ type, placeholder = "", value, onChange, className = "" }: InputProps) {
    return (
        <input
            type={type}
            placeholder={placeholder}
            value={value}
            onChange={onChange}
            className={`custom-input ${className}`}
        />
    );
}