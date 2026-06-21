import type { CustomBarTooltipProps } from "../types";
import styles from "./BarTooltip.module.css";

export function BarTooltip({
    active,
    payload,
    label,
}: CustomBarTooltipProps) {
    if (!active || !payload?.length) return null;

    return (
        <div className={styles.tooltip}>
            <p className={styles.label}>{label}</p>

            {payload.map((entry) => (
                <div key={entry.dataKey} className={styles.item}>
                    <span>{entry.name ?? entry.dataKey}</span>
                    <span className={styles.value}>
                        {Number(entry.value).toLocaleString()}
                    </span>
                </div>
            ))}
        </div>
    );
}