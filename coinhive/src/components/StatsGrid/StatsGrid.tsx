import { type ReactNode, type CSSProperties } from "react";
import styles from "./StatsGrid.module.css";

type StatsGridProps = {
    children: ReactNode;
    columns?: number; 
    gap?: number;    
    style?: CSSProperties;
};

export function StatsGrid({ children, columns = 3, gap = 16, style }: StatsGridProps) {
    return (
        <div
            style={{
                "--columns": columns,
                "--gap": `${gap}px`,
                ...style,
            } as React.CSSProperties}
            className={styles.statsGrid}
        >
            {children}
        </div>
    );
}