import styles from "./SettingsSection.module.css";
import SettingsToggle from "../SettingsToggle/SettingsToggle";
import type { SettingsSectionProps } from "./types";

const SettingsSection = ({ title, description, badge, items, toggles, onToggle }: SettingsSectionProps) => {
    return (
        <section className={styles.sectionCard}>
            <div className={styles.sectionHeader}>
                <div>
                    <h3 className={styles.sectionTitle}>{title}</h3>
                    <p className={styles.sectionDescription}>{description}</p>
                </div>
                {badge ? <span className={styles.badge}>{badge}</span> : null}
            </div>

            <div className={styles.itemList}>
                {items.map((item) => {
                    if (item.type === "toggle") {
                        return (
                            <SettingsToggle
                                key={item.id}
                                label={item.label}
                                description={item.description}
                                checked={Boolean(toggles[item.id])}
                                onToggle={() => onToggle(item.id)}
                            />
                        );
                    }

                    return (
                        <div key={item.id} className={styles.valueItem}>
                            <div className={styles.itemText}>
                                <span className={styles.itemLabel}>{item.label}</span>
                                <span className={styles.itemDescription}>{item.description}</span>
                            </div>
                            <span className={styles.valuePill}>{item.value}</span>
                        </div>
                    );
                })}
            </div>
        </section>
    );
};

export default SettingsSection;
