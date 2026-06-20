import {
    PanelsTopLeft,
    ChartNoAxesCombined,
    PiggyBank,
    ChartBarStacked,
    CircleUserRound ,
    ArrowLeftRight,
    Settings ,
    HandCoins,
    LogOut,
} from "lucide-react";

export const NAV_ITEMS = [
    {
        name: "Dashboard",
        icon: <PanelsTopLeft size={20} />,
    },
    {
        name: "Analytics",
        icon: < ChartNoAxesCombined size={20} />,
    },
    {
        name: "Transactions",
        icon: < ArrowLeftRight size={20} />,
    },
    {
        name: "Budgets",
        icon: <PiggyBank size={20} />,
    },
    {
        name: "Accounts",
        icon: <CircleUserRound size={20} />,
    },
    {
        name: "Categories",
        icon: < ChartBarStacked size={20} />,
    },
    {
        name: "Savings",
        icon: < HandCoins size={20} />,
    },
    {
        name: "Settings",
        icon: < Settings  size={20} />,
    },
    {
        name: "Log out",
        icon: < LogOut  size={20} />,
    },
];

export const DEFAULT_PROFILE = {
    firstName: "Jordan",
    lastName: "Mitchell",
    role: "Software Engineer",
    currency: "USD",
    bio: "Financial analyst and personal finance enthusiast. Tracking goals since 2021. Building toward early financial independence.",
};