import type { PieChartProps } from '../PieChart/types';
import type { PieLegendProps } from '../PieChartLegend/types';

export const STATIC_DATA  = [
    { name: 'Group A', value: 400, fill: 'var(--primary-500)' },
    { name: 'Group B', value: 300, fill: 'var(--primary-400)' },
    { name: 'Group C', value: 300, fill: 'var(--primary-300)' },
    { name: 'Group D', value: 200, fill: 'var(--primary-100)' },
];
export const PIE_LEGEND_DATA: PieLegendProps = {
    data : STATIC_DATA,
}
export const PIE_CHART_DATA: PieChartProps = {
    data: STATIC_DATA,
    innerRadius: '40%',
    outerRadius: '80%',
    cornerRadius: '10%',
    padding: 5,
    dataKey : 'value',
    isAnimationActive : true
}
