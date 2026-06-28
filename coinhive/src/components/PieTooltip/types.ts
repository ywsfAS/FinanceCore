export interface PieTooltipPayloadEntry {
    name?: string;
    value?: number;
    color?: string;
    fill?: string;
    percent?: number;
    payload?: {
        name?: string;
        value?: number;
        color?: string;
        percent?: number;
    };
}

export interface CustomPieTooltipProps {
    active?: boolean;
    payload?: PieTooltipPayloadEntry[];
}