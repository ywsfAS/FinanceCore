import type { CustomTooltipProps } from "../types";
import styles from "./PieTooltip.module.css";

export function PieTooltip({ active, payload }: CustomTooltipProps) {
    if (!active || !payload?.length) return null;
    const entry = payload[0];
    const name = entry.name ?? entry.payload?.name;
    const value = entry.value ?? entry.payload?.value;
    return (
        <div className={styles.tooltip}>
            <div className={styles.label}>{name}</div>
            <div className={styles.value}>${value?.toLocaleString()}</div>
        </div>
    );
}