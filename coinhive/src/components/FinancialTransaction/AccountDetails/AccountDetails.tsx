import styles from "./AccountDetails.module.css";
import type { DetailRow } from "../types";

type Props = {
    details: DetailRow[];
};

export default function AccountDetails({ details }: Props) {
    return (
        <div className={styles.card}>
            <p className={styles.title}>Account Details</p>

            <div className={styles.list}>
                {details.map((d) => (
                    <div className={styles.row} key={d.key}>
                        <div className={styles.key}>
                            <span className={styles.dot} />
                            {d.key}
                        </div>

                        {d.pill ? (
                            <span className={`${styles.pill} ${styles[d.pill.variant]}`}>
                                {d.pill.label}
                            </span>
                        ) : (
                            <span className={styles.value}>{d.value}</span>
                        )}
                    </div>
                ))}
            </div>
        </div>
    );
}