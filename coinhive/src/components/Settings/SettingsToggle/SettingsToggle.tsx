import styles from "./SettingsToggle.module.css";
import type { SettingsToggleProps } from "./types";

const SettingsToggle = ({ label, description, checked, onToggle }: SettingsToggleProps) => {
    return (
        <div className={styles.item}>
            <div className={styles.itemText}>
                <span className={styles.itemLabel}>{label}</span>
                <span className={styles.itemDescription}>{description}</span>
            </div>

            <button
                type="button"
                className={`${styles.toggle} ${checked ? styles.toggleOn : ""}`}
                aria-pressed={checked}
                onClick={onToggle}
            />
        </div>
    );
};

export default SettingsToggle;
