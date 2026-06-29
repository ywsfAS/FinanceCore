import { PieChart, Pie, Tooltip, ResponsiveContainer } from "recharts";
import { PieTooltip } from '../PieTooltip/PieTooltip';
import type {PieChartProps} from './types'
export function CostumePieChart({data, innerRadius  ,outerRadius , cornerRadius , padding , dataKey  , isAnimationActive} : PieChartProps) {
    return (
        <div style={{ width: '100%', maxWidth: '250px', aspectRatio: 1 }}>
            <ResponsiveContainer width="100%" height="100%">
                <PieChart>
                    <Pie
                        data={data}
                        innerRadius={innerRadius }
                        outerRadius={outerRadius }
                        cornerRadius={cornerRadius }
                        paddingAngle={padding}
                        dataKey={dataKey }
                        isAnimationActive={isAnimationActive}
                    />
                    <Tooltip content={<PieTooltip />} />
                </PieChart>
            </ResponsiveContainer>
        </div>
    );
}
