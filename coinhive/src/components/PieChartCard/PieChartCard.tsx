import { PIE_CHART_DATA , PIE_LEGEND_DATA} from "./constants";
import { PieChartLegend } from '../PieChartLegend/PieChartLegend';
import { CostumePieChart } from '../PieChart/PieChart';
import styles from './PieChartCard.module.css';
import type {PieChartCardProps} from './types';
export function PieChartCard({title,subtitle } : PieChartCardProps) {
    const { data, cornerRadius, outerRadius, innerRadius, isAnimationActive, dataKey, padding } = PIE_CHART_DATA;
    const { data : legendData} = PIE_LEGEND_DATA;
    return (
        <div className={styles.card}>
            <div className={styles.content }>
                <h2 className={styles.title }>{title}</h2>
                <p className={styles.subtitles}>{subtitle}</p>
            </div>
            <CostumePieChart data={data} innerRadius={innerRadius} outerRadius={outerRadius} dataKey={dataKey} padding={padding} cornerRadius={cornerRadius} isAnimationActive={isAnimationActive} />
            <PieChartLegend data={legendData} />
        </div>
    );
}