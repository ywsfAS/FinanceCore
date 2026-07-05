import styles from "./SettingsHeader.module.css";
import type { SettingsHeaderProps } from "./types";

const SettingsHeader = ({ title, subtitle }: SettingsHeaderProps) => {
    return (
        <section className={styles.headerCard}>
            <h2 className={styles.headerTitle}>{title}</h2>
            <p className={styles.headerText}>{subtitle}</p>
        </section>
    );
};

export default SettingsHeader;
