import { BarChart, Bar, XAxis, YAxis, Tooltip, CartesianGrid, ResponsiveContainer , Legend , ReferenceLine} from "recharts";
import "../../styles/utility.css";
import { BarTooltip } from "../BarTooltip/BarTooltip";
import { ChartLegend} from "../Legend/Legend";
import { type BarChartCardProps, DEFAULT_CONFIG, DEFAULT_DATA, DEFAULT_KEY1 , DEFAULT_KEY2, type Data } from "./constants"; 


const calculatePeak = (data : Data , keys  : string[]) : number => {
    return Math.max(...data.flatMap((item) => keys.map(key => Math.abs(Number(item[key] || 0)))));
}
export function BarChartCard({ data = DEFAULT_DATA, config = DEFAULT_CONFIG, dataKey1 = DEFAULT_KEY1, dataKey2 = DEFAULT_KEY2 }: BarChartCardProps) {
    const peak = calculatePeak(data,[dataKey1,dataKey2]);
    return (

        <ResponsiveContainer width="100%" height="70%">
            <BarChart data={data} barGap={-60} barSize={60}>
                <CartesianGrid strokeDasharray="3 3" vertical={false} />

                <XAxis dataKey="name" axisLine={false} tickLine={false} tick={config}
                />

                <YAxis width={40} domain={[-peak, peak]} axisLine={false} tickLine={false} tick={config} />

                <Tooltip content={<BarTooltip/>} />
                <Legend content={<ChartLegend/>} />
                <ReferenceLine y={0} stroke="#E2E8F0" strokeWidth={4} />
                <Bar
                    dataKey={dataKey1 }
                    fill="#3B82F6"
                    radius={[16, 16, 0, 0]}
                />
                
                <Bar
                    dataKey={dataKey2 }
                    fill="#60A5FA"
                    radius={[16, 16, 0, 0]}
                />
            </BarChart>
        </ResponsiveContainer>
    );
}