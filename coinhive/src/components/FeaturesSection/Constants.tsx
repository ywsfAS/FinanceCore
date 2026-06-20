import {
    BarChart2,
    Target,
    Bell,
    ShieldCheck,
} from "lucide-react";

export interface Feature {
    icon: React.ReactNode;
    title: string;
    description: string;
    color: string;
}

export const features: Feature[] = [
    {
        icon: <BarChart2 size={22} strokeWidth={1.6} />,
        title: "Your money, finally clear",
        description:
            "A real-time dashboard that shows exactly where your money goes — no guessing, no spreadsheets, just clarity.",
        color: "#0f6e56",
    },
    {
        icon: <Target size={22} strokeWidth={1.6} />,
        title: "Goals that actually move you",
        description:
            "Set financial goals and watch real progress build up daily with smart, visual tracking that keeps you consistent.",
        color: "#185fa5",
    },
    {
        icon: <Bell size={22} strokeWidth={1.6} />,
        title: "Know before it becomes a problem",
        description:
            "Instant alerts for overspending, bills, and unusual activity — so you stay ahead, not surprised.",
        color: "#534ab7",
    },
    {
        icon: <ShieldCheck size={22} strokeWidth={1.6} />,
        title: "Security you don’t have to think about",
        description:
            "Bank-level encryption and strict protection systems keep your data safe while you focus on your money, not risk.",
        color: "#085041",
    },
];