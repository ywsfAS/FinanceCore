import { useEffect, useRef } from 'react';
import {
    Chart,
    BarController, BarElement,
    DoughnutController, ArcElement,
    CategoryScale, LinearScale,
    Tooltip, Legend,
} from 'chart.js';
import styles from './ChartsSection.module.css';

Chart.register(
    BarController, BarElement,
    DoughnutController, ArcElement,
    CategoryScale, LinearScale,
    Tooltip, Legend,
);

const PIE_LEGEND = [
    { color: 'var(--chart-purple)', label: 'Housing 38%' },
    { color: 'var(--chart-blue)', label: 'Food 22%' },
    { color: 'var(--chart-green)', label: 'Transport 15%' },
    { color: 'var(--chart-orange)', label: 'Subs 12%' },
];

const BAR_LEGEND = [
    { color: 'var(--chart-purple)', label: 'Income' },
    { color: 'var(--chart-red)', label: 'Expenses' },
];

export default function ChartsSection() {
    const barRef = useRef<HTMLCanvasElement>(null);
    const barChart = useRef<Chart | null>(null);

    useEffect(() => {
        if (!barRef.current) return;

        barChart.current?.destroy();

        barChart.current = new Chart(barRef.current, {
            type: 'bar',
            data: {
                labels: ['Jan', 'Feb', 'Mar', 'Apr'],
                datasets: [
                    {
                        label: 'Income',
                        data: [7400, 7900, 7600, 8450],
                        backgroundColor: 'rgba(109,40,217,0.7)',
                        borderRadius: 3,
                        borderSkipped: false,
                        barThickness: 38,
                    },
                    {
                        label: 'Expenses',
                        data: [3600, 4100, 3500, 3720],
                        backgroundColor: 'rgba(248,113,113,0.6)',
                        borderRadius: 3,
                        borderSkipped: false,
                        barThickness: 38,
                    },
                ],
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: { display: false },
                    tooltip: { mode: 'index', intersect: false },
                },
                scales: {
                    x: {
                        ticks: { color: '#475569', font: { size: 11 } },
                        grid: { display: true, color: 'rgba(148, 163, 184, 0.15)'  },
                        border: { display: true },
                    },
                    y: {
                        ticks: {
                            color: '#475569',
                            font: { size: 11 },
                            callback: (value) => '$' + Number(value).toLocaleString(),
                        },
                        grid: { color: 'rgba(148, 163, 184, 0.15)' },
                        border: { display: true },
                    },
                },
                animation: { duration: 900 },
            },
        });

        return () => {
            barChart.current?.destroy();
        };
    }, []);

    return (
        <div className={styles.section}>

            {/* Bar */}
            <div className={styles.chartCard}>
                <div className={styles.chartTitle}>Monthly Spending Trend</div>
                <div className={styles.chartDesc}>Jan – Apr 2026 · income vs expenses</div>
                <div className={styles.legend}>
                    {BAR_LEGEND.map(({ color, label }) => (
                        <span key={label} className={styles.legendItem}>
                            <span className={styles.dot} style={{ background: color }} />
                            {label}
                        </span>
                    ))}
                </div>
                <div className={styles.canvasWrap}>
                    <canvas ref={barRef} />
                </div>
            </div>
        </div>
    );
}
