import { BarChart, Bar, XAxis, YAxis, Tooltip, CartesianGrid, ResponsiveContainer , Legend , ReferenceLine} from "recharts";
import type { MonthlyEntry, SummaryItem } from "../types";
import "../../styles/utility.css";
import { BarTooltip } from "../BarTooltip/BarTooltip";
import styles from "./BarChartCard.module.css";

const data = [
    { name: "Jan", income: 100, expense: -50 },
    { name: "Feb", income: 940, expense: -120 },
    { name: "Mar", income: 650, expense: -30 },
    { name: "Apr", income: 1000, expense: -224 },
    { name: "May", income: 10, expense: -9 },
];
const tickConfig = {
    fill: "#99B2C6",
    fontSize: 14,
    fontWeight: 500,
    fontFamily : "Plus Jakarta Sans",
}
const max = Math.max(...data.flatMap((d) => [Math.abs(d.income), Math.abs(d.expense)]));
export function BarChartCard() {
    return (

        <ResponsiveContainer width="100%" height="100%">
            <BarChart data={data} barGap={-60} barSize={60}>
                <CartesianGrid strokeDasharray="3 3" vertical={false} />

                <XAxis dataKey="name" axisLine={false} tickLine={false} tick={tickConfig}
                />

                <YAxis width={40} domain={[-max, max]} axisLine={false} tickLine={false} tick={tickConfig} />

                <Tooltip content={<BarTooltip/>} />
                <Legend />
                <ReferenceLine y={0} stroke="#E2E8F0" strokeWidth={4} />
                <Bar
                    dataKey="income"
                    fill="#3B82F6"
                    radius={[16, 16, 0, 0]}
                />
                
                <Bar
                    dataKey="expense"
                    fill="#60A5FA"
                    radius={[16, 16, 0, 0]}
                />
            </BarChart>
        </ResponsiveContainer>
    );
}