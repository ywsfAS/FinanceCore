import type { LegendProps } from "recharts";
import styles from "./ChartLegend.module.css";

export function ChartLegend({ payload }: LegendProps) {
    if (!payload) return null;

    return (
        <div className={styles.legend}>
            {payload.map((entry) => (
                <div
                    key={entry.value}
                    className={styles.item}
                >
                    <span
                        className={styles.color}
                        style={{
                            backgroundColor: entry.color,
                        }}
                    />

                    <span className={styles.label}>
                        {entry.value}
                    </span>
                </div>
            ))}
        </div>
    );
}