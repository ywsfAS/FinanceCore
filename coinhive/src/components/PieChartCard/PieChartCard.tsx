import { useState } from "react";
import { PieChart, Pie, Cell, Tooltip, ResponsiveContainer } from "recharts";
import type { CategoryEntry } from "../types";
import { PieTooltip } from "../PieTooltip/PieTooltip";
import styles from "./PieChartCard.module.css";

type Props = {
    data: CategoryEntry[];
    colors: string[];
    icons: string[];
};

export function PieChartCard({ data, colors, icons }: Props) {
    const total = data.reduce((sum, d) => sum + d.value, 0);
    const [activeIndex, setActiveIndex] = useState<number | null>(null);
    const activeCat = activeIndex !== null ? data[activeIndex] : null;

    return (
        <div className={styles.card}>
            <p className={styles.title}>By Category</p>
            <div className={styles.pieWrap}>
                <ResponsiveContainer width="100%" height={200}>
                    <PieChart>
                        <defs>
                            {colors.map((color, i) => (
                                <radialGradient key={i} id={`pie-grad-${i}`} cx="50%" cy="50%" r="50%">
                                    <stop offset="0%" stopColor={color} stopOpacity={1} />
                                    <stop offset="100%" stopColor={color} stopOpacity={0.7} />
                                </radialGradient>
                            ))}
                        </defs>
                        <Pie
                            data={data}
                            dataKey="value"
                            cx="50%"
                            cy="50%"
                            innerRadius={55}
                            outerRadius={85}
                            paddingAngle={3}
                            onMouseEnter={(_, index) => setActiveIndex(index)}
                            onMouseLeave={() => setActiveIndex(null)}
                        >
                            {data.map((_, index) => (
                                <Cell
                                    key={index}
                                    fill={`url(#pie-grad-${index})`}
                                    stroke="transparent"
                                    style={{
                                        filter:
                                            activeIndex === index
                                                ? `drop-shadow(0 0 8px ${colors[index]})`
                                                : "none",
                                        transition: "filter 0.2s, opacity 0.2s",
                                        opacity: activeIndex !== null && activeIndex !== index ? 0.5 : 1,
                                    }}
                                />
                            ))}
                        </Pie>
                        <Tooltip content={<PieTooltip />} />
                    </PieChart>
                </ResponsiveContainer>

                <div className={styles.centerStat}>
                    <div className={styles.centerValue}>
                        {activeCat ? `$${activeCat.value}` : `$${total}`}
                    </div>
                    <div className={styles.centerLabel}>
                        {activeCat ? activeCat.name : "Total"}
                    </div>
                </div>
            </div>

            <div className={styles.legend}>
                {data.map((item, index) => (
                    <div
                        key={index}
                        className={`${styles.legendItem}${activeIndex === index ? ` ${styles.active}` : ""}`}
                        onMouseEnter={() => setActiveIndex(index)}
                        onMouseLeave={() => setActiveIndex(null)}
                    >
                        <span className={styles.legendIcon}>{icons[index]}</span>
                        <span
                            className={styles.legendDot}
                            style={{
                                background: colors[index],
                                boxShadow: `0 0 8px ${colors[index]}`,
                            }}
                        />
                        <span className={styles.legendLabel}>{item.name}</span>
                        <span className={styles.legendValue}>${item.value}</span>
                        <span className={styles.legendPct} style={{ color: colors[index] }}>
                            {Math.round((item.value / total) * 100)}%
                        </span>
                    </div>
                ))}
            </div>
        </div>
    );
}