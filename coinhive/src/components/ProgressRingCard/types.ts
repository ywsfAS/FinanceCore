import { type LucideIcon } from "lucide-react";


export interface ProgressRingCardProps {
    icon?: LucideIcon;
    title: string;
    label: string;
    subtitle: string;
    maxValue: number;
    value: number;
    radius?: number;
}