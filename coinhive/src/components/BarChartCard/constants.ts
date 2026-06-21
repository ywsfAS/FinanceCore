export type Data = Record<string, string | number>[];
export const DEFAULT_DATA : Data  = [
    { name: "Jan", income: 400, expense: -500 },
    { name: "Feb", income: 940, expense: -120 },
    { name: "Mar", income: 650, expense: -30 },
    { name: "Apr", income: 1000, expense: -224 },
    { name: "May", income: 1020, expense: -900 },
];
export const DEFAULT_CONFIG : tickConfig = {
    fill: "#99B2C6",
    fontSize: 14,
    fontWeight: 500,
    fontFamily : "Plus Jakarta Sans",
}
export const DEFAULT_KEY1 = "income";
export const DEFAULT_KEY2 = "expense";

export interface tickConfig {
    fill: string;
    fontSize: number;
    fontWeight: number;
    fontFamily: string;
}
export interface BarChartCardProps {
    data?: Record<string, string | number>[];
    config?: tickConfig;
    dataKey1?: string;
    dataKey2?: string;
}
