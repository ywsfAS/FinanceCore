import type { SettingsHeaderData, SettingsSection } from "./types";

export const SETTINGS_HEADER: SettingsHeaderData = {
    title: "Settings",
    subtitle: "Customize your experience and keep your account preferences up to date.",
    btnName: "Save Changes",
};

export const SETTINGS_SECTIONS: SettingsSection[] = [
    {
        id: "notifications",
        title: "Notifications",
        description: "Choose what updates you want to receive.",
        badge: "Recommended",
        items: [
            {
                id: "email-updates",
                label: "Email updates",
                description: "Receive weekly summaries and important account alerts.",
                type: "toggle",
            },
            {
                id: "push-reminders",
                label: "Push reminders",
                description: "Get reminders for upcoming bills and savings milestones.",
                type: "toggle",
            },
        ],
    },
    {
        id: "preferences",
        title: "Preferences",
        description: "Adjust the way your dashboard feels and behaves.",
        items: [
            {
                id: "currency",
                label: "Preferred currency",
                description: "Used for balances, reports, and transaction summaries.",
                type: "text",
                value: "USD",
            },
            {
                id: "timezone",
                label: "Timezone",
                description: "Align your activity list with your local time.",
                type: "text",
                value: "UTC-05:00",
            },
        ],
    },
    {
        id: "security",
        title: "Security",
        description: "Protect your account with a few simple controls.",
        items: [
            {
                id: "2fa",
                label: "Two-factor authentication",
                description: "Add an extra layer of protection to your sign-in.",
                type: "toggle",
            },
            {
                id: "session-alerts",
                label: "Session alerts",
                description: "Get notified when a new device logs into your account.",
                type: "toggle",
            },
        ],
    },
];
