import styles from "./HowItWorks.module.css";
import { steps } from "./Constants";

interface HowItWorksProps {
    title: string;
    subtitle: string;
}

const HowItWorks = ({ title, subtitle }: HowItWorksProps) => {
    return (
        <section className={styles.wrapper}>
            <div className={styles.header}>
                <h2 className={styles.title}>{title}</h2>
                <p className={styles.subtitle}>{subtitle}</p>
            </div>

            <div className={styles.steps}>
                {steps.map((step, index) => (
                    <article
                        key={step.number}
                        className={styles.step}
                    >
                        <div className={styles.stepLeft}>
                            <div className={styles.circle}>
                                {step.icon}
                            </div>

                            {index < steps.length - 1 && (
                                <div className={styles.line} />
                            )}
                        </div>

                        <div className={styles.body}>
                            <span className={styles.number}>
                                {step.number}
                            </span>

                            <h3 className={styles.stepTitle}>
                                {step.title}
                            </h3>

                            <p className={styles.stepDesc}>
                                {step.description}
                            </p>
                        </div>
                    </article>
                ))}
            </div>
        </section>
    );
};

export default HowItWorks;