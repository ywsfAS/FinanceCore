import type { CSSProperties, ReactNode } from "react";
import styles from "./Card.module.css";

type variant = 'default' | 'col'; 
type theme = 'light' | 'dark';
type CardProps = {
    children: ReactNode;
    style?: CSSProperties;
    variant?: variant;
    theme?: theme;
    size?: string;
    className?: string;

}

export default function Card({ children, variant = 'default', style, className = '', size = '' , theme = 'light'}: CardProps) {
    const variantStyle = `${styles.card} ${styles[variant]} ${styles[size]} ${styles[theme]} ${className}`;
    return (
        <div className={variantStyle} style={style} >
            {children }    
        </div>
    )
}