import styles from "./PieChartLegend.module.css";
import type { PieLegendProps } from './types';

export function PieChartLegend({
    data = [],
    unit = "%",
    accentColor = "var(--primary-300)",
} : PieLegendProps) {
    if (!data.length) return null;

    return (
        <ul className={styles.legend}>
            {data.map(({ name, value, color }) => {
                const rowColor = color || accentColor;

                return (
                    <li key={name} className={styles.legendItem}>
                        <span className={styles.label}>
                            <span
                                className={styles.dot}
                                style={{ backgroundColor: rowColor }}
                            />
                            <span className={styles.name} style={{ color: rowColor }}>
                                {name}
                            </span>
                        </span>

                        <span className={styles.value} style={{ color: rowColor }}>
                            {value}
                            {unit}
                        </span>
                    </li>
                );
            })}
        </ul>
    );
}