export const HEADER = {
    title: "Budgets",
    subtitle: "Track spending limits and stay ahead of your goals",
    btnName: "New Budget",
};

export const BUDGET_PERIODS = [
    { value: "Weekly", label: "Weekly" },
    { value: "Monthly", label: "Monthly" },
    { value: "Yearly", label: "Yearly" },
];

export const CURRENCY_OPTIONS = [
    { value: "USD", label: "USD" },
    { value: "EUR", label: "EUR" },
    { value: "MAD", label: "MAD" },
];

export const MOCK_BUDGETS = [
    {
        id: "1",
        name: "Groceries",
        amount: 600,
        currency: "USD",
        categoryName: "Food",
        period: "Monthly" as const,
        startDate: "2026-07-01",
        endDate: "2026-07-31",
        progress: 72,
    },
    {
        id: "2",
        name: "Travel",
        amount: 1200,
        currency: "USD",
        categoryName: "Lifestyle",
        period: "Monthly" as const,
        startDate: "2026-07-01",
        endDate: "2026-07-31",
        progress: 48,
    },
    {
        id: "3",
        name: "Cloud Tools",
        amount: 180,
        currency: "USD",
        categoryName: "Work",
        period: "Monthly" as const,
        startDate: "2026-07-01",
        endDate: "2026-07-31",
        progress: 88,
    },
];
