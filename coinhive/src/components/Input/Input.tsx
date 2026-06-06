import React from "react";
import styles from  "./Input.module.css";
import { CircleX } from 'lucide-react';
import {useState} from 'react';
interface InputProps extends React.InputHTMLAttributes<HTMLInputElement>  {
    variant?: 'primary' | 'secondary';
    error?: string;
    label?: string;
    borderStyle?: 'dashed' | 'solid';
    inputSize?: 'sm' | 'md' | 'lg';
    InfoMessage?: string;

}
export default function Input({variant = 'primary',inputSize='md',InfoMessage,borderStyle = 'solid',id,className,error,label,type,...props}: InputProps) {
    const [infoSection, setInfoSection] = useState(false);

    const isFile = type === 'file';
    const inputStyle = `
    ${styles.customInput}
    ${variant ? styles[variant] : ""}
    ${error ? styles.error : ""}
    ${borderStyle ? styles[borderStyle] : ""}
    ${inputSize ? styles[inputSize] : ""}
    ${className}
    ${isFile ? styles['hidden'] : ""}
    `;
    const errorMessageStyle = `
        ${styles.errorMessage}`;

    const labelStyle = `${styles.label} ${isFile ? styles.uploadBox : ""}`;

    return (
        <div className={styles.inputWrapper}>
            {label && <label htmlFor={id} className={labelStyle}>{label}</label> }
            <input
            id={id}
            className={inputStyle}
            type={type}
            {...props}
            />
            {error && <div className={errorMessageStyle}>{error}<CircleX onClick={() => { setInfoSection((prev) => !prev) }} size={15} /></div>}
            {infoSection && <div className={styles.infoSection}>{InfoMessage}</div>}
        </div>
    );
}