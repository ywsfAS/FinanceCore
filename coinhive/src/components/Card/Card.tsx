import type { CSSProperties, ReactNode } from "react";
import styles from "./Card.module.css";


type CardProps = {
    children: ReactNode;
    style?: CSSProperties;
    variant?: string;

}
export default function Card({ children, variant = 'default', style }: CardProps) {
    const variantStyle = `${styles.card} ${styles[variant]}`;
    return (
        <div className={variantStyle} style={style} >
            {children }    
        </div>
    )
}