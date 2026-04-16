import styles from './OverviewHeader.module.css';

export default function OverviewHeader() {
    return (
        <div className={styles.header}>
            <h3 className={styles.title}>Financial Overview</h3>
            <span className={styles.desc}>April 2026 · Last updated just now</span>
        </div>
    );
}