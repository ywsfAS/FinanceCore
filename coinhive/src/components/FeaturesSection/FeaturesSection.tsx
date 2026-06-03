// FeaturesSection.tsx
import React, { useEffect, useRef } from "react";
import styles from "./FeaturesSection.module.css";
import {
    BarChart2,
    Target,
    Bell,
    Building2,
    Cpu,
    ShieldCheck,
} from "lucide-react";

interface Feature {
    icon: React.ReactNode;
    title: string;
    description: string;
    color: string;
}

interface FeaturesSectionProps {
    title: string;
    description: string;
}

const features: Feature[] = [
    {
        icon: <BarChart2 size={22} strokeWidth={1.6} />,
        title: "Real-time dashboard",
        description:
            "See your full financial picture at a glance — balances, budgets, net worth, and spending trends updated live.",
        color: "#0f6e56",
    },
    {
        icon: <Target size={22} strokeWidth={1.6} />,
        title: "Smart goal tracking",
        description:
            "Set savings goals, track progress with visual milestones, and get AI-driven recommendations to reach them faster.",
        color: "#185fa5",
    },
    {
        icon: <Bell size={22} strokeWidth={1.6} />,
        title: "Intelligent alerts",
        description:
            "Get notified before you overspend, when bills are due, or when unusual activity is detected on your accounts.",
        color: "#534ab7",
    },
    {
        icon: <Building2 size={22} strokeWidth={1.6} />,
        title: "Multi-account sync",
        description:
            "Connect all your banks, cards, wallets, and brokerages in one place with read-only, bank-grade secure access.",
        color: "#854f0b",
    },
    {
        icon: <Cpu size={22} strokeWidth={1.6} />,
        title: "AI spending insights",
        description:
            "Our model analyzes your habits and surfaces personalized tips — from subscription audits to smarter budget splits.",
        color: "#a32d2d",
    },
    {
        icon: <ShieldCheck size={22} strokeWidth={1.6} />,
        title: "Bank-grade security",
        description:
            "256-bit AES encryption, biometric login, SOC 2 Type II certification, and zero data selling. Ever.",
        color: "#085041",
    },
];

const FeaturesSection: React.FC<FeaturesSectionProps> = ({ title, description }) => {
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
                <p className={styles.subtitle}>{description}</p>
            </div>
            <div className={styles.grid}>
                {features.map((f, i) => (
                    <div
                        key={f.title}
                        className={styles.card}
                        style={{ transitionDelay: `${i * 70}ms` }}
                    >
                        <div
                            className={styles.icon}
                        >
                            {f.icon}
                        </div>
                        <h3 className={styles.cardTitle}>{f.title}</h3>
                        <p className={styles.cardDesc}>{f.description}</p>
                        <a href="#" className={styles.cardLink} style={{ color: f.color }}>
                            Learn more
                            <svg width="13" height="13" viewBox="0 0 24 24" fill="none"
                                stroke="currentColor" strokeWidth="2.2" strokeLinecap="round"
                                strokeLinejoin="round">
                                <path d="M5 12h14M12 5l7 7-7 7" />
                            </svg>
                        </a>
                    </div>
                ))}
            </div>
        </section>
    );
};

export default FeaturesSection;