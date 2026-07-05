import type { SavingGoal, SavingGoalsFilters, SavingGoalStatus } from "./types";

export const HEADER = {
    title: "Savings Goals",
    subtitle: "Track your progress toward every financial target.",
    btnName: "New Goal",
};

export const INITIAL_FILTERS : SavingGoalsFilters = {
    search: "",
    currency: "",
    status: "",
};

export const CURRENCIES = [
    { value: "", label: "All Currencies" },
    { value: "USD", label: "USD" },
    { value: "EUR", label: "EUR" },
    { value: "MAD", label: "MAD" },
];

export const SAVING_STATUSES: { value: SavingGoalStatus | ""; label: string }[] = [
    { value: "", label: "All Statuses" },
    { value: "active", label: "Active" },
    { value: "paused", label: "Paused" },
    { value: "completed", label: "Completed" },
    { value: "cancelled", label: "Cancelled" },
];

export const MOCK_GOALS: SavingGoal[] = [
    {
        id: "goal-1",
        name: "Emergency Fund",
        description: "Build a 6-month safety buffer for unexpected expenses.",
        targetAmount: 12000,
        currentAmount: 5400,
        currency: "USD",
        targetDate: "2025-12-31",
        status: "active",
    },
    {
        id: "goal-2",
        name: "Vacation Trip",
        description: "Save for the summer family getaway.",
        targetAmount: 3800,
        currentAmount: 1120,
        currency: "EUR",
        targetDate: "2025-07-10",
        status: "paused",
    },
    {
        id: "goal-3",
        name: "Home Renovation",
        description: "Upgrade the kitchen and living room for better comfort.",
        targetAmount: 8500,
        currentAmount: 7500,
        currency: "USD",
        targetDate: "2026-03-15",
        status: "completed",
    },
];