import React, { useEffect, useRef } from "react";
import styles from "./ValueSection.module.css";

interface Value {
    icon: string;
    title: string;
    description: string;
}

const values: Value[] = [
    {
        icon: "🔒",
        title: "Security First",
        description:
            "Bank-grade 256-bit encryption, SOC 2 Type II certified, and zero data selling — your financial data stays yours.",
    },
    {
        icon: "🔭",
        title: "Transparency",
        description:
            "No hidden fees, no fine print. Every feature, every cost, every data practice is clearly communicated upfront.",
    },
    {
        icon: "📈",
        title: "Growth Mindset",
        description:
            "We build tools that evolve with your goals — whether you're starting out or managing complex portfolios.",
    },
    {
        icon: "🤝",
        title: "Trust",
        description:
            "Trusted by over 120,000 users. We earn that trust every day through reliability, honesty, and accountability.",
    },
    {
        icon: "⚡",
        title: "Speed & Precision",
        description:
            "Real-time data, instant sync, and lightning-fast analytics so you always have accurate insights when you need them.",
    },
    {
        icon: "🌍",
        title: "Accessibility",
        description:
            "Financial clarity shouldn't be a privilege. We design for everyone — simple interfaces, multiple languages, fair pricing.",
    },
];

const ValuesSection: React.FC = () => {
    const ref = useRef<HTMLDivElement>(null);

    useEffect(() => {
        const el = ref.current;
        if (!el) return;

        const observer = new IntersectionObserver(
            ([entry]) => {
                if (entry.isIntersecting) {
                    el.classList.add(styles.visible);
                    observer.disconnect();
                }
            },
            { threshold: 0.1 }
        );

        observer.observe(el);
        return () => observer.disconnect();
    }, []);

    return (
        <section className={styles.wrapper} ref={ref}>
            <div className={styles.header}>
                <span className={styles.eyebrow}>What We Stand For</span>
                <h2 className={styles.title}>Our Core Values</h2>
            </div>

            <div className={styles.grid}>
                {values.map((value, i) => (
                    <div
                        key={value.title}
                        className={styles.card}
                        style={{ transitionDelay: `${i * 70}ms` }}
                    >
                        <span className={styles.icon}>{value.icon}</span>
                        <h3 className={styles.cardTitle}>{value.title}</h3>
                        <p className={styles.cardDesc}>{value.description}</p>
                    </div>
                ))}
            </div>
        </section>
    );
};

export default ValuesSection;