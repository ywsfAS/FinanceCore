import React, { useEffect, useRef } from "react";
import styles from "./CtaSection.module.css";

const CtaSection: React.FC = () => {
    const ref = useRef<HTMLDivElement>(null);

    useEffect(() => {
        const el = ref.current;
        if (!el) return;
        const observer = new IntersectionObserver(
            ([entry]) => { if (entry.isIntersecting) { el.classList.add(styles.visible); observer.disconnect(); } },
            { threshold: 0.15 }
        );
        observer.observe(el);
        return () => observer.disconnect();
    }, []);

    return (
        <section className={styles.wrapper} ref={ref}>
            <div className={styles.card}>
                <div className={styles.blobLeft} aria-hidden="true" />
                <div className={styles.blobRight} aria-hidden="true" />
                <div className={styles.inner}>
                    <span className={styles.eyebrow}>Get Started Today</span>
                    <h2 className={styles.title}>
                        Your Financial Future<br />Starts Right Now
                    </h2>
                    <p className={styles.subtitle}>
                        Join 120,000+ users who've taken control of their money with
                        FinVault. Free forever. No credit card required.
                    </p>
                    <div className={styles.actions}>
                        <a href="#" className={styles.btnPrimary}>Create Free Account →</a>
                        <a href="#" className={styles.btnSecondary}>Talk to Sales</a>
                    </div>
                    <div className={styles.guarantees}>
                        <span>✓ No credit card</span>
                        <span>✓ Cancel anytime</span>
                        <span>✓ SOC 2 Certified</span>
                        <span>✓ 99.9% uptime</span>
                    </div>
                </div>
            </div>
        </section>
    );
};

export default CtaSection;