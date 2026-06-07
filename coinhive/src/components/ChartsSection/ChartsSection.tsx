import { useEffect, useRef } from 'react';
import { useUserMonthlyTrend } from '../../hooks/Reports/useUserMonthlyTrend';
import {
    Chart,
    BarController, BarElement,
    CategoryScale, LinearScale,
    Tooltip, Legend,
} from 'chart.js';
import styles from './ChartsSection.module.css';
import type { MonthlyUserTrendParams } from '../../services/reportService';

Chart.register(
    BarController, BarElement, 
    CategoryScale, LinearScale,
    Tooltip, Legend,
);

const BAR_LEGEND = [
    { color: 'var(--chart-purple)', label: 'Income' },
    { color: 'var(--chart-red)', label: 'Expenses' },
];

export default function ChartsSection() {

    const param: MonthlyUserTrendParams = {
        month : 4
    }; 
     const { data, isLoading, isError, error } = useUserMonthlyTrend(param);

    const barRef = useRef<HTMLCanvasElement>(null);
    const barChart = useRef<Chart | null>(null);

    useEffect(() => {
        if (!barRef.current) return;

        barChart.current?.destroy();

        barChart.current = new Chart(barRef.current, {
            type: 'bar',
            data: {
                labels: data.map(x => x.month),
                datasets: [
                    {
                        label: 'Income',
                        data: data.map(x => x.totalExpense),
                        backgroundColor: 'rgba(109,40,217,0.7)',
                        borderRadius: 3,
                        borderSkipped: false,
                        barThickness: 38,
                    },
                    {
                        label: 'Expenses',
                        data: data.map(x => x.totalIncome),
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

    if (isLoading) return <div>loading...</div>;
    if (isError) return <div>{error.message}</div>;

    if (!data) return <div>No data provided</div>;
    console.log("chart", data);


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
