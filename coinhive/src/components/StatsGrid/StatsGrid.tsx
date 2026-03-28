import { ReactNode, CSSProperties } from "react";
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
            className={styles.statsGrid}
            style={{
                gridTemplateColumns: `repeat(${columns}, 1fr)`,
                gap: `${gap}px`,
                ...style,
            }}
        >
            {children}
        </div>
    );
}