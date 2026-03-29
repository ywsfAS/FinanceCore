import type { CSSProperties, ReactNode } from "react";
import styles from "./Card.module.css";


type CardProps = {
    children: ReactNode;
    style?: CSSProperties;
    variant?: string;
    size?: string;
    className?: string;

}
export default function Card({ children, variant = 'default', style, className = '', size = '' }: CardProps) {
    const variantStyle = `${styles.card} ${styles[variant]} ${styles[size]} ${className}`;
    return (
        <div className={variantStyle} style={style} >
            {children }    
        </div>
    )
}