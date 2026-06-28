import { PieChart, Pie, Tooltip, ResponsiveContainer } from "recharts";
import { data } from "./constants";
import {PieTooltip} from '../PieTooltip/PieTooltip';
export function PieChartCard() {
    return (
        <div  style={{ width: '100%', maxWidth: '250px', aspectRatio: 1 }}>
            <ResponsiveContainer width="100%" height="60%">
                <PieChart>
                    <Pie
                        data={data}
                        innerRadius="80%"
                        outerRadius="100%"
                        cornerRadius="50%"
                        fill="#8884d8"
                        paddingAngle={5}
                        dataKey="value"
                        isAnimationActive={true}
                    />
                    <Tooltip content={<PieTooltip />} />
                </PieChart>
            </ResponsiveContainer>
        </div>
    );
}