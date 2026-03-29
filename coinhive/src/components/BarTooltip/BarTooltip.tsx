import type { CustomBarTooltipProps } from "../types";
import styles from "./BarTooltip.module.css";

export function BarTooltip({ active, payload, label }: CustomBarTooltipProps) {
    if (!active || !payload?.length) return null;
    return (
        <div className={styles.tooltip}>
            <div className={styles.label}>{label}</div>
            <div className={styles.value}>${payload[0].value.toLocaleString()}</div>
        </div>
    );
}