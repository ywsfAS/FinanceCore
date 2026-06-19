import styles from "./ActiviyInsight.module.css";
import Phone from "../../assets/Silver.png";

export interface ActivityInsightProps {
    title: string;
    description: string;
}

const ActiviyInsight = ({
    title,
    description,
}: ActivityInsightProps) => {
    return (
        <section className={styles.wrapper}>
            <h2 className={styles.title}>
                {title}
            </h2>

            <p className={styles.description}>
                {description}
            </p>

            <div className={styles.images}>

                <img
                    src={Phone}
                    alt="Mobile application"
                    className={styles.phone}
                />
            </div>
        </section>
    );
};

export default ActiviyInsight;