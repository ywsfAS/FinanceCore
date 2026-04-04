import React, { useEffect, useRef } from "react";
import styles from "./LogoSection.module.css";

const logos = [
    "Goldman Sachs", "Stripe", "Revolut", "N26",
    "Wise", "Plaid", "Robinhood", "Coinbase",
];

const LogoSection: React.FC = () => {
    const ref = useRef<HTMLDivElement>(null);

    useEffect(() => {
        const el = ref.current;
        if (!el) return;
        const observer = new IntersectionObserver(
            ([entry]) => { if (entry.isIntersecting) { el.classList.add(styles.visible); observer.disconnect(); } },
            { threshold: 0.1 }
        );
        observer.observe(el);
        return () => observer.disconnect();
    }, []);

    return (
        <section className={styles.wrapper} ref={ref}>
            <p className={styles.label}>Trusted by teams from the world's best fintech companies</p>
            <div className={styles.logos}>
                {logos.map((name) => (
                    <div key={name} className={styles.logo}>{name}</div>
                ))}
            </div>
        </section>
    );
};

export default LogoSection;