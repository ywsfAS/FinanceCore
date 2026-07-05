export interface SettingItem {
    id: string;
    label: string;
    description: string;
    type: "toggle" | "text";
    value?: string;
}

export interface SettingsSection {
    id: string;
    title: string;
    description: string;
    badge?: string;
    items: SettingItem[];
}

export interface SettingsHeaderData {
    title: string;
    subtitle: string;
    btnName: string;
}
