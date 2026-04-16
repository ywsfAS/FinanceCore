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
    const pieRef = useRef<HTMLCanvasElement>(null);
    const barChart = useRef<Chart | null>(null);
    const pieChart = useRef<Chart | null>(null);

    useEffect(() => {
        if (!barRef.current || !pieRef.current) return;

        barChart.current?.destroy();
        pieChart.current?.destroy();

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
                        barThickness: 18,
                    },
                    {
                        label: 'Expenses',
                        data: [3600, 4100, 3500, 3720],
                        backgroundColor: 'rgba(248,113,113,0.6)',
                        borderRadius: 3,
                        borderSkipped: false,
                        barThickness: 18,
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
                        grid: { display: false },
                        border: { display: false },
                    },
                    y: {
                        ticks: {
                            color: '#475569',
                            font: { size: 11 },
                            callback: (value) => '$' + Number(value).toLocaleString(),
                        },
                        grid: { color: 'rgba(255,255,255,0.04)' },
                        border: { display: false },
                    },
                },
                animation: { duration: 900 },
            },
        });
           pieChart.current = new Chart(pieRef.current, {
            type: 'doughnut',
            data: {
                labels: ['Housing', 'Food', 'Transport', 'Subscriptions', 'Other'],
                datasets: [{
                    data: [38, 22, 15, 12, 13],
                    backgroundColor: [
                        '#6d28d9', 
                        '#3b82f6', 
                        '#22c55e', 
                        '#f59e0b', 
                        '#ef4444', 
                    ],
                    borderWidth: 1,
                    borderColor: 'rgba(10,14,26,0.8)',
                    hoverOffset: 6,
                }],
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                cutout: '68%',
                plugins: {
                    legend: { display: false },
                    tooltip: {
                        callbacks: {
                            label: (c) => ` ${c.label}: ${c.parsed}%`,
                        },
                    },
                },
                animation: { animateRotate: true, duration: 1000 },
            },
        });

        return () => {
            barChart.current?.destroy();
            pieChart.current?.destroy();
        };
    }, []);

    return (
        <div className={styles.section}>
            {/* Pie */}
            <div className={styles.chartCard}>
                <div className={styles.chartTitle}>Spending by Category</div>
                <div className={styles.chartDesc}>April 2026</div>
                <div className={styles.legend}>
                    {PIE_LEGEND.map(({ color, label }) => (
                        <span key={label} className={styles.legendItem}>
                            <span className={styles.dot} style={{ background: color }} />
                            {label}
                        </span>
                    ))}
                </div>
                <div className={styles.canvasWrap}>
                    <canvas ref={pieRef} />
                </div>
            </div>

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
