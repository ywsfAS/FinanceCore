import {
    BarChart2,
    Target,
    Bell,
    ShieldCheck,
} from "lucide-react";

export interface Feature {
    direction: Record<string, number>;
    icon: React.ReactNode;
    title: string;
    description: string;
    color: string;
    waterLevel: number;
}
export const waterConfig = (waterLevel: number) => ({
    initial: {
        y: "100%",
    },

    animate: {
        y: `${waterLevel}%`,
        x: ["-5%", "0%", "-5%"],
    },

    transition: {
        y: {
            type: "spring",
            stiffness: 600,
            damping: 20,
        },

        x: {
            duration: 5,
            repeat: Infinity,
            ease: "easeInOut",
        },
    },
});
export const animationConfig = (index: number, direction: { x?: number; y?: number }) => ({
    initial: {
        opacity: 0,
        ...direction,
    },

    whileInView: {
        opacity: 1,
        x: 0,
        y: 0,
    },

    whileHover: {
        scale: 1.04,
        transition: {
            type: "spring",
            stiffness: 400,
            damping: 25,
        },
    },

    viewport: {
        once: true,
        amount: 0.3,
    },

    transition: {
        duration: 0.3,
        delay: index * 0.1,
    },
});
export const features: Feature[] = [
    {
        direction: { x: 800 },
        icon: <BarChart2 size={22} strokeWidth={1.6} />,
        title: "Your money, finally clear",
        description:
            "A real-time dashboard that shows exactly where your money goes — no guessing, no spreadsheets, just clarity.",
        color: "#0f6e56",
        waterLevel: 0
    },
    {
        direction: { x: 800 },
        icon: <Target size={22} strokeWidth={1.6} />,
        title: "Goals that actually move you",
        description:
            "Set financial goals and watch real progress build up daily with smart, visual tracking that keeps you consistent.",
        color: "#185fa5",
        waterLevel: 0
    },
    {
        direction: { x: 800 },
        icon: <Bell size={22} strokeWidth={1.6} />,
        title: "Know before it becomes a problem",
        description:
            "Instant alerts for overspending, bills, and unusual activity — so you stay ahead, not surprised.",
        color: "#534ab7",
        waterLevel: 0
    },
    {
        direction: { x: 800 },
        icon: <ShieldCheck size={22} strokeWidth={1.6} />,
        title: "Security you don’t have to think about",
        description:
            "Bank-level encryption and strict protection systems keep your data safe while you focus on your money, not risk.",
        color: "#085041",
        waterLevel: 0
    },
];