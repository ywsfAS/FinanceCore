import { Topbar } from "../Topbar/Topbar";
import { HeroBalance } from "../HeroBalance/HeroBalance";
import { StatCard } from "../StatCard/StatCard";
import Card from "../Card/Card";
import { type ReactNode } from "react";
import { StatsGrid } from "../StatsGrid/StatsGrid";
import "../../styles/utility.css";

// Mini stats for HeroBalance
const heroStats = [
    { label: "Income", value: "8,240", positive: true },
    { label: "Expenses", value: "3,180", positive: false },
    { label: "Net", value: "5,060", positive: true },
];

// Stat cards
const stats: {
    label: string;
    value: string;
    change: string;
    direction: "up" | "down";
    icon: ReactNode;
    iconVariant?: "green" | "red" | "blue";
}[] = [
        { label: "Monthly Income", value: "$8,240", change: "+12.4%", direction: "up", icon: "💰", iconVariant: "green" },
        { label: "Monthly Expenses", value: "$3,180", change: "-6.1%", direction: "down", icon: "💸", iconVariant: "red" },
        { label: "Savings Rate", value: "61.4%", change: "+3.2%", direction: "up", icon: "📈", iconVariant: "blue" },
    ];

export default function FinancialCard() {
    return (
        <Card className='py-2 px-2 flex-col m-'>
            {/* Topbar */}
            <Topbar username="YS" date="Tuesday, April 2025" />

            {/* Hero Balance Card */}
            <HeroBalance title="Total Balance" totalBalance="12,450.00" stats={heroStats} />
     
            {/* Stat Cards Grid */}
            <StatsGrid columns={3} gap={16}>
                {stats.map((s) => (
                   <StatCard
                     label={s.label}
                     value={s.value}
                     change={s.change}
                     direction={s.direction}
                     icon={s.icon}
                     iconVariant={s.iconVariant}
                  />
                ))}
            </StatsGrid>
        </Card>
    );
}