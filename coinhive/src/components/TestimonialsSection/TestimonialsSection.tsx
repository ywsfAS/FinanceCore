import React, { useEffect, useRef } from "react";
import styles from "./TestimonialsSection.module.css";

interface Testimonial {
    quote: string;
    name: string;
    role: string;
    initials: string;
    color: string;
    stars: number;
}

const testimonials: Testimonial[] = [
    {
        quote: "FinVault replaced four apps I was using. My net worth went up 18% the first year just because I could actually see where my money was going.",
        name: "Layla Hassan",
        role: "Freelance Designer, Dubai",
        initials: "LH",
        color: "#10b981",
        stars: 5,
    },
    {
        quote: "The AI insights are genuinely useful — it caught a subscription I forgot about and flagged that I was overspending on dining before I even noticed.",
        name: "Tom Eriksson",
        role: "Software Engineer, Stockholm",
        initials: "TE",
        color: "#3b82f6",
        stars: 5,
    },
    {
        quote: "I've tried Mint, YNAB, and Personal Capital. FinVault is the first one that didn't feel like homework. The UX is just clean and it works.",
        name: "Priya Nair",
        role: "Product Manager, London",
        initials: "PN",
        color: "#8b5cf6",
        stars: 5,
    },
    {
        quote: "Security was my biggest concern. Knowing it's SOC 2 certified and read-only access made the decision easy. My bank data never felt so safe.",
        name: "Carlos Mendez",
        role: "CFO, Mexico City",
        initials: "CM",
        color: "#f59e0b",
        stars: 5,
    },
    {
        quote: "I hit my emergency fund goal in 11 months using the goal tracker. Having a visual progress bar genuinely changed my savings behavior.",
        name: "Amara Diallo",
        role: "Nurse, Paris",
        initials: "AD",
        color: "#ef4444",
        stars: 5,
    },
    {
        quote: "Our startup uses FinVault for expense tracking across the team. Saved us from hiring a bookkeeper for the first two years.",
        name: "James Wu",
        role: "Co-Founder, Singapore",
        initials: "JW",
        color: "#06b6d4",
        stars: 5,
    },
];

const TestimonialsSection: React.FC = () => {
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
                <span className={styles.eyebrow}>What Users Say</span>
                <h2 className={styles.title}>Real People, Real Results</h2>
                <p className={styles.subtitle}>
                    Over 120,000 users have transformed how they manage money with FinVault.
                </p>
            </div>

            <div className={styles.grid}>
                {testimonials.map((t, i) => (
                    <div key={t.name} className={styles.card} style={{ transitionDelay: `${i * 70}ms` }}>
                        <div className={styles.stars}>
                            {"★".repeat(t.stars)}
                        </div>
                        <p className={styles.quote}>"{t.quote}"</p>
                        <div className={styles.author}>
                            <div className={styles.avatar} style={{ background: t.color + "18", color: t.color }}>
                                {t.initials}
                            </div>
                            <div>
                                <span className={styles.name}>{t.name}</span>
                                <span className={styles.role}>{t.role}</span>
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </section>
    );
};

export default TestimonialsSection;