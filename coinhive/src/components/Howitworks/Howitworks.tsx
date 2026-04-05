import React, { useEffect, useRef } from "react";
import styles from "./HowItWorks.module.css";

type Step = {
    number: string;
    title: string;
    description: string;
    icon: string;
}

const steps : Step[] = [
    {
        number: "01",
        title: "Create Your Account",
        description: "Sign up in under 60 seconds. No credit card required. Just your email and a password.",
        icon: "✍️",
    },
    {
        number: "02",
        title: "Connect Your Accounts",
        description: "Securely link your banks, cards, and wallets using read-only Plaid integration.",
        icon: "🔗",
    },
    {
        number: "03",
        title: "Set Your Goals",
        description: "Tell us what you're working toward — an emergency fund, a vacation, early retirement.",
        icon: "🎯",
    },
    {
        number: "04",
        title: "Watch It Work",
        description: "Get real-time insights, smart alerts, and monthly reports that keep you on track.",
        icon: "📈",
    },
];
interface HowItWorksProps {
    title: string,
    subtitle : string
}


const HowItWorks: React.FC<HowItWorksProps> = ({title , subtitle }) => {
    const ref = useRef < HTMLDivElement > (null);

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
            <div className={styles.header}>
                <span className={styles.eyebrow}>How It Works</span>
                <h2 className={styles.title}>{title}</h2>
                <p className={styles.subtitle}>
                    {subtitle}
                </p>
            </div>

            <div className={styles.steps}>
                {steps.map((step, i) => (
                    <div key={step.number} className={styles.step} style={{ transitionDelay: `${i * 100}ms` }}>
                        <div className={styles.stepLeft}>
                            <div className={styles.stepNumber}>{step.number}</div>
                            {i < steps.length - 1 && <div className={styles.connector} />}
                        </div>
                        <div className={styles.stepBody}>
                            <div className={styles.stepIcon}>{step.icon}</div>
                            <div>
                                <h3 className={styles.stepTitle}>{step.title}</h3>
                                <p className={styles.stepDesc}>{step.description}</p>
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </section>
    );
};

export default HowItWorks;