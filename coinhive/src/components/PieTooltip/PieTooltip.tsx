import type { CustomPieTooltipProps } from "../types";
import styles from "./PieTooltip.module.css";

const currencyFormatter = new Intl.NumberFormat("en-US", {
    style: "currency",
    currency: "USD",
});

export function PieTooltip({ active, payload }: CustomPieTooltipProps) {
    if (!active || !payload?.length) return null;

    const entry = payload[0];
    const name = entry.name ?? entry.payload?.name ?? "";
    const value = Number(entry.value ?? entry.payload?.value ?? 0);
    const color =
        entry.payload?.color ?? entry.color ?? entry.fill ?? "var(--primary-500)";
    const percent = entry.percent ?? entry.payload?.percent;

    return (
        <div className={styles.tooltip}>
            <span className={styles.swatch} style={{ background: color }} />
            <div className={styles.body}>
                <span className={styles.name}>{name}</span>
                <div className={styles.valueRow}>
                    <span className={styles.value}>
                        {currencyFormatter.format(value)}
                    </span>
                    {percent !== undefined && (
                        <span className={styles.percent}>
                            {Math.round(percent * 100)}%
                        </span>
                    )}
                </div>
            </div>
        </div>
    );
}