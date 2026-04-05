import React, { useEffect, useRef } from "react";
import styles from "./FeaturesSection.module.css";

interface Feature {
    icon: string;
    title: string;
    description: string;
    color: string;
    tag: string;
}
interface FeaturesSectionProps {
    title: string,
    tag: string,
    description : string,
}

const features: Feature[] = [
    {
        icon: "📊",
        title: "Real-Time Dashboard",
        description: "See your full financial picture at a glance — balances, budgets, net worth, and spending trends updated live.",
        color: "#10b981",
        tag: "Core",
    },
    {
        icon: "🎯",
        title: "Smart Goal Tracking",
        description: "Set savings goals, track progress with visual milestones, and get AI-driven recommendations to reach them faster.",
        color: "#3b82f6",
        tag: "Popular",
    },
    {
        icon: "🔔",
        title: "Intelligent Alerts",
        description: "Get notified before you overspend, when bills are due, or when unusual activity is detected on your accounts.",
        color: "#8b5cf6",
        tag: "Smart",
    },
    {
        icon: "🏦",
        title: "Multi-Account Sync",
        description: "Connect all your banks, cards, wallets, and brokerages in one place with read-only, bank-grade secure access.",
        color: "#f59e0b",
        tag: "Sync",
    },
    {
        icon: "🤖",
        title: "AI Spending Insights",
        description: "Our model analyzes your habits and surfaces personalized tips — from subscription audits to smarter budget splits.",
        color: "#ef4444",
        tag: "New",
    },
    {
        icon: "🔒",
        title: "Bank-Grade Security",
        description: "256-bit AES encryption, biometric login, SOC 2 Type II certification, and zero data selling. Ever.",
        color: "#06b6d4",
        tag: "Security",
    },
];

const FeaturesSection: React.FC<FeaturesSectionProps> = ({title , tag , description }) => {
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
            <div className={styles.header}>
                <span className={styles.eyebrow}>{tag}</span>
                <h2 className={styles.title}>{title}</h2>
                <p className={styles.subtitle}>
                    {description}

                </p>
            </div>

            <div className={styles.grid}>
                {features.map((f, i) => (
                    <div
                        key={f.title}
                        className={styles.card}
                        style={{ transitionDelay: `${i * 70}ms` }}
                    >
                        <div className={styles.cardTop}>
                            <div className={styles.icon} style={{ background: f.color + "18", color: f.color }}>
                                {f.icon}
                            </div>
                            <span className={styles.tag} style={{ color: f.color, background: f.color + "12", border: `1px solid ${f.color}30` }}>
                                {f.tag}
                            </span>
                        </div>
                        <h3 className={styles.cardTitle}>{f.title}</h3>
                        <p className={styles.cardDesc}>{f.description}</p>
                        <a href="#" className={styles.cardLink} style={{ color: f.color }}>
                            Learn more →
                        </a>
                    </div>
                ))}
            </div>
        </section>
    );
};

export default FeaturesSection;