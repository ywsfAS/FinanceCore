// HowItWorks.tsx
import React, { useEffect, useRef } from "react";
import styles from "./HowItWorks.module.css";
import { UserPlus, Link2, Target, TrendingUp } from "lucide-react";

type Step = {
    number: string;
    title: string;
    description: string;
    icon: React.ReactNode;
};

const steps: Step[] = [
    {
        number: "01",
        title: "Create your account",
        description: "Sign up in under 60 seconds. No credit card required — just your email and a password.",
        icon: <UserPlus size={20} strokeWidth={1.6} />,
    },
    {
        number: "02",
        title: "Connect your accounts",
        description: "Securely link your banks, cards, and wallets using read-only Plaid integration.",
        icon: <Link2 size={20} strokeWidth={1.6} />,
    },
    {
        number: "03",
        title: "Set your goals",
        description: "Tell us what you're working toward — an emergency fund, a vacation, early retirement.",
        icon: <Target size={20} strokeWidth={1.6} />,
    },
    {
        number: "04",
        title: "Watch it work",
        description: "Get real-time insights, smart alerts, and monthly reports that keep you on track.",
        icon: <TrendingUp size={20} strokeWidth={1.6} />,
    },
];

interface HowItWorksProps {
    title: string;
    subtitle: string;
}

const HowItWorks: React.FC<HowItWorksProps> = ({ title, subtitle }) => {
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
                <h2 className={styles.title}>{title}</h2>
                <p className={styles.subtitle}>{subtitle}</p>
            </div>
            <div className={styles.steps}>
                {steps.map((step, i) => (
                    <div
                        key={step.number}
                        className={styles.step}
                        style={{ transitionDelay: `${i * 100}ms` }}
                    >
                        <div className={styles.stepLeft}>
                            <div className={styles.circle}>{step.icon}</div>
                            {i < steps.length - 1 && <div className={styles.line} />}
                        </div>
                        <div className={styles.body}>
                            <span className={styles.number}>{step.number}</span>
                            <h3 className={styles.stepTitle}>{step.title}</h3>
                            <p className={styles.stepDesc}>{step.description}</p>
                        </div>
                    </div>
                ))}
            </div>
        </section>
    );
};

export default HowItWorks;