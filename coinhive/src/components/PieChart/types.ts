
export interface PieChartProps {
    data: Record<string, string | number>[];
    innerRadius: string;
    outerRadius: string;
    cornerRadius: string;
    fill?: string;
    padding: number;
    dataKey: string; 
    isAnimationActive: boolean;
}
