import styles from "./TestimonialsSection.module.css";
import { MessageSquareQuote } from "lucide-react";
import { testimonials, animationConfig } from "./Constants";
import { motion } from 'motion/react';
const TestimonialsSection = () => {
    return (
        <section className={styles.wrapper}>
            <div className={styles.header}>
                <h2 className={styles.title}>
                    Take Control With Confidence
                </h2>

                <p className={styles.subtitle}>
                    Over 120,000 users have transformed how they manage money
                    with FinanceCore.
                </p>
            </div>

            <div className={styles.grid}>
                {testimonials.map((t, index) => (
                    <motion.article
                        {...animationConfig(index)}
                        key={t.name}
                        className={styles.card}
                    >
                        <MessageSquareQuote strokeWidth={2} className={styles.quoteIcon} />


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
                    </motion.article>
                ))}
            </div>
        </section>
    );
};

export default TestimonialsSection;