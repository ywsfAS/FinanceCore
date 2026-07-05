
import { useMemo, useState } from "react";
import styles from "./Settings.module.css";
import { SETTINGS_HEADER, SETTINGS_SECTIONS } from "./constants";
import SettingsHeader from "./SettingsHeader/SettingsHeader";
import Button from "../Button/Button";
import SettingsSection from "./SettingsSection/SettingsSection";

export const Settings = () => {
    const [toggles, setToggles] = useState<Record<string, boolean>>({
        "email-updates": true,
        "push-reminders": false,
        "2fa": true,
        "session-alerts": false,
    });

    const sections = useMemo(() => SETTINGS_SECTIONS, []);

    const toggleSetting = (id: string) => {
        setToggles((prev) => ({ ...prev, [id]: !prev[id] }));
    };

    return (
        <div className={styles.wrapper}>
            <SettingsHeader title={SETTINGS_HEADER.title} subtitle={SETTINGS_HEADER.subtitle} />

            <div className={styles.sections}>
                {sections.map((section) => (
                    <SettingsSection
                        key={section.id}
                        title={section.title}
                        description={section.description}
                        badge={section.badge}
                        items={section.items}
                        toggles={toggles}
                        onToggle={toggleSetting}
                    />
                ))}
            </div>

            <div className={styles.footerCard}>
                <Button variant="primary" size="medium">
                    {SETTINGS_HEADER.btnName}
                </Button>
            </div>
        </div>
    );
};

export default Settings;
