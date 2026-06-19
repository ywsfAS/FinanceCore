import { UserPlus, Link2, Target, TrendingUp } from "lucide-react";

export type Step = {
    number: string;
    title: string;
    description: string;
    icon: React.ReactNode;
};

export const steps: Step[] = [
    {
        number: "01",
        title: "Create your account",
        description: "Sign up in under 60 seconds. No credit card required — just your email and a password.",
        icon: <UserPlus size={20} strokeWidth={1.6} />,
    },
    {
        number: "02",
        title: "Connect your accounts",
        description: "Securely link your banks, cards, and wallets using read-only Plaid integration.",
        icon: <Link2 size={20} strokeWidth={1.6} />,
    },
    {
        number: "03",
        title: "Set your goals",
        description: "Tell us what you're working toward — an emergency fund, a vacation, early retirement.",
        icon: <Target size={20} strokeWidth={1.6} />,
    },
    {
        number: "04",
        title: "Watch it work",
        description: "Get real-time insights, smart alerts, and monthly reports that keep you on track.",
        icon: <TrendingUp size={20} strokeWidth={1.6} />,
    },
];
