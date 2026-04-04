import React, { useEffect, useRef } from "react";
import styles from "./HeroSection.module.css";

const HeroSection: React.FC = () => {
    const ref = useRef<HTMLDivElement>(null);

    useEffect(() => {
        const el = ref.current;
        if (el) requestAnimationFrame(() => el.classList.add(styles.visible));
    }, []);

    return (
        <section className={styles.hero} ref={ref}>
            <div className={styles.gridOverlay} aria-hidden="true" />
            <div className={styles.blobLeft} aria-hidden="true" />
            <div className={styles.blobRight} aria-hidden="true" />

            <div className={styles.inner}>
                <a href="#" className={styles.announcement}>
                    <span className={styles.announcementBadge}>New</span>
                    <span>AI-powered spending insights are live →</span>
                </a>

                <h1 className={styles.title}>
                    Take Full Control of<br />
                    <span className={styles.accent}>Your Financial Life</span>
                </h1>

                <p className={styles.tagline}>
                    FinVault brings your accounts, budgets, goals, and investments into
                    one secure dashboard — so you always know where you stand and where
                    you're headed.
                </p>

                <div className={styles.actions}>
                    <a href="#" className={styles.btnPrimary}>Start for Free →</a>
                    <a href="#" className={styles.btnSecondary}>
                        <span className={styles.playIcon}>▶</span> Watch Demo
                    </a>
                </div>

                <div className={styles.trust}>
                    <div className={styles.avatarStack}>
                        {["#10b981", "#3b82f6", "#8b5cf6", "#f59e0b"].map((c, i) => (
                            <div key={i} className={styles.avatar} style={{ background: c, zIndex: 4 - i }} />
                        ))}
                    </div>
                    <span className={styles.trustText}>
                        Trusted by <strong>120,000+</strong> users across 34 countries
                    </span>
                </div>

                <div className={styles.mockup} aria-hidden="true">
                    <div className={styles.mockupBar}>
                        <span className={styles.dot} style={{ background: "#ef4444" }} />
                        <span className={styles.dot} style={{ background: "#f59e0b" }} />
                        <span className={styles.dot} style={{ background: "#10b981" }} />
                        <span className={styles.mockupUrl}>app.finvault.io/dashboard</span>
                    </div>
                    <div className={styles.mockupBody}>
                        <div className={styles.mockupSidebar}>
                            {["Dashboard", "Transactions", "Goals", "Analytics", "Settings"].map(item => (
                                <div key={item} className={`${styles.mockupNavItem} ${item === "Dashboard" ? styles.mockupNavActive : ""}`}>{item}</div>
                            ))}
                        </div>
                        <div className={styles.mockupContent}>
                            <div className={styles.mockupCards}>
                                {[
                                    { label: "Net Worth", value: "$84,320", color: "#10b981" },
                                    { label: "Monthly Spend", value: "$3,210", color: "#3b82f6" },
                                    { label: "Savings Rate", value: "34%", color: "#8b5cf6" },
                                ].map(card => (
                                    <div key={card.label} className={styles.mockupCard}>
                                        <span className={styles.mockupCardLabel}>{card.label}</span>
                                        <span className={styles.mockupCardValue} style={{ color: card.color }}>{card.value}</span>
                                        <div className={styles.mockupCardBar}>
                                            <div className={styles.mockupCardFill} style={{ background: card.color, width: card.label === "Net Worth" ? "72%" : card.label === "Monthly Spend" ? "55%" : "34%" }} />
                                        </div>
                                    </div>
                                ))}
                            </div>
                            <div className={styles.mockupChart}>
                                {[40, 65, 50, 80, 60, 90, 75, 95, 70, 88, 82, 100].map((h, i) => (
                                    <div key={i} className={styles.mockupBar2} style={{ height: `${h}%`, background: i === 11 ? "#10b981" : `rgba(16,185,129,${0.15 + i * 0.06})` }} />
                                ))}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};

export default HeroSection;
