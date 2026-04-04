import React, { useEffect, useRef, useState } from "react";
import styles from "./PricingSection.module.css";

interface Plan {
    name: string;
    monthlyPrice: number;
    yearlyPrice: number;
    description: string;
    features: string[];
    cta: string;
    highlighted: boolean;
    badge?: string;
}

const plans: Plan[] = [
    {
        name: "Free",
        monthlyPrice: 0,
        yearlyPrice: 0,
        description: "For individuals just getting started with their finances.",
        features: [
            "Up to 2 connected accounts",
            "Basic spending overview",
            "1 savings goal",
            "Monthly summary email",
            "Mobile app access",
        ],
        cta: "Get Started Free",
        highlighted: false,
    },
    {
        name: "Pro",
        monthlyPrice: 12,
        yearlyPrice: 9,
        description: "For people serious about building real financial clarity.",
        features: [
            "Unlimited account connections",
            "Real-time dashboard & alerts",
            "Unlimited goals",
            "AI spending insights",
            "Investment tracking",
            "Priority support",
            "Custom categories",
        ],
        cta: "Start Pro Trial",
        highlighted: true,
        badge: "Most Popular",
    },
    {
        name: "Enterprise",
        monthlyPrice: 49,
        yearlyPrice: 39,
        description: "For teams, startups, and financial professionals.",
        features: [
            "Everything in Pro",
            "Up to 25 team members",
            "Shared dashboards",
            "API access",
            "Dedicated account manager",
            "SSO & custom roles",
            "1-hour SLA support",
        ],
        cta: "Contact Sales",
        highlighted: false,
    },
];

const PricingSection: React.FC = () => {
    const ref = useRef<HTMLDivElement>(null);
    const [yearly, setYearly] = useState(false);

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
                <span className={styles.eyebrow}>Pricing</span>
                <h2 className={styles.title}>Simple, Transparent Pricing</h2>
                <p className={styles.subtitle}>
                    No hidden fees. No surprises. Upgrade or cancel anytime.
                </p>
                <div className={styles.toggle}>
                    <span className={!yearly ? styles.toggleActive : styles.toggleInactive}>Monthly</span>
                    <button
                        className={`${styles.toggleBtn} ${yearly ? styles.toggleBtnOn : ""}`}
                        onClick={() => setYearly(!yearly)}
                        aria-label="Toggle yearly billing"
                    >
                        <span className={styles.toggleKnob} />
                    </button>
                    <span className={yearly ? styles.toggleActive : styles.toggleInactive}>
                        Yearly <span className={styles.saveBadge}>Save 25%</span>
                    </span>
                </div>
            </div>

            <div className={styles.grid}>
                {plans.map((plan, i) => (
                    <div
                        key={plan.name}
                        className={`${styles.card} ${plan.highlighted ? styles.highlighted : ""}`}
                        style={{ transitionDelay: `${i * 90}ms` }}
                    >
                        {plan.badge && <div className={styles.badge}>{plan.badge}</div>}
                        <div className={styles.planHeader}>
                            <span className={styles.planName}>{plan.name}</span>
                            <div className={styles.price}>
                                <span className={styles.currency}>$</span>
                                <span className={styles.amount}>
                                    {yearly ? plan.yearlyPrice : plan.monthlyPrice}
                                </span>
                                <span className={styles.period}>/mo</span>
                            </div>
                            <p className={styles.planDesc}>{plan.description}</p>
                        </div>

                        <ul className={styles.features}>
                            {plan.features.map((f) => (
                                <li key={f} className={styles.feature}>
                                    <span className={styles.check}>✓</span>
                                    {f}
                                </li>
                            ))}
                        </ul>

                        <a
                            href="#"
                            className={`${styles.cta} ${plan.highlighted ? styles.ctaHighlighted : ""}`}
                        >
                            {plan.cta}
                        </a>
                    </div>
                ))}
            </div>
        </section>
    );
};

export default PricingSection;