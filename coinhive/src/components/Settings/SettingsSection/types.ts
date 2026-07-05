import type { SettingItem } from "../types";

export interface SettingsSectionProps {
    title: string;
    description: string;
    badge?: string;
    items: SettingItem[];
    toggles: Record<string, boolean>;
    onToggle: (id: string) => void;
}
