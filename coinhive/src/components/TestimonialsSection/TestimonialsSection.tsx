import styles from "./TestimonialsSection.module.css";
import { Quote } from "lucide-react";
import { testimonials } from "./Constants";

const TestimonialsSection = () => {
    return (
        <section className={styles.wrapper}>
            <div className={styles.header}>
                <h2 className={styles.title}>
                    Real People, Real Results
                </h2>

                <p className={styles.subtitle}>
                    Over 120,000 users have transformed how they manage money
                    with FinanceCore.
                </p>
            </div>

            <div className={styles.grid}>
                {testimonials.map((t) => (
                    <article
                        key={t.name}
                        className={styles.card}
                    >
                        <Quote
                            className={styles.quoteIcon}
                        />

                        <p className={styles.quote}>
                            "{t.quote}"
                        </p>

                        <div className={styles.author}>
                            <div
                                className={styles.avatar}
                                style={{
                                    background:
                                        t.color + "15",
                                    color: t.color,
                                }}
                            >
                                {t.initials}
                            </div>

                            <div>
                                <span className={styles.name}>
                                    {t.name}
                                </span>

                                <span className={styles.role}>
                                    {t.role}
                                </span>
                            </div>
                        </div>
                    </article>
                ))}
            </div>
        </section>
    );
};

export default TestimonialsSection;