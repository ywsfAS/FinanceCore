import {
    BarChart2,
    Target,
    Bell,
    Building2,
    Cpu,
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
        title: "Real-time dashboard",
        description:
            "See your full financial picture at a glance — balances, budgets, net worth, and spending trends updated live.",
        color: "#0f6e56",
    },
    {
        icon: <Target size={22} strokeWidth={1.6} />,
        title: "Smart goal tracking",
        description:
            "Set savings goals, track progress with visual milestones, and get AI-driven recommendations to reach them faster.",
        color: "#185fa5",
    },
    {
        icon: <Bell size={22} strokeWidth={1.6} />,
        title: "Intelligent alerts",
        description:
            "Get notified before you overspend, when bills are due, or when unusual activity is detected on your accounts.",
        color: "#534ab7",
    },
    {
        icon: <Building2 size={22} strokeWidth={1.6} />,
        title: "Multi-account sync",
        description:
            "Connect all your banks, cards, wallets, and brokerages in one place with read-only, bank-grade secure access.",
        color: "#854f0b",
    },
    {
        icon: <Cpu size={22} strokeWidth={1.6} />,
        title: "AI spending insights",
        description:
            "Our model analyzes your habits and surfaces personalized tips — from subscription audits to smarter budget splits.",
        color: "#a32d2d",
    },
    {
        icon: <ShieldCheck size={22} strokeWidth={1.6} />,
        title: "Bank-grade security",
        description:
            "256-bit AES encryption, biometric login, SOC 2 Type II certification, and zero data selling. Ever.",
        color: "#085041",
    },
];

