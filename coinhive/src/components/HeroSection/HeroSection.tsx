import React, { useEffect, useRef } from "react";
import styles from "./HeroSection.module.css";

const dashboardData = [
    { label: "Net Worth", value: "$84,320", color: "#10b981" },
    { label: "Monthly Spend", value: "$3,210", color: "#3b82f6" },
    { label: "Savings Rate", value: "34%", color: "#8b5cf6" },
];
const navItems= ["Dashboard", "Transactions", "Goals", "Analytics", "Settings"];
const colors = ["#10b981", "#3b82f6", "#8b5cf6", "#f59e0b"];
const barHeight = [40, 65, 50, 80, 60, 90, 75, 95, 70, 88, 82, 100];
interface HeroSectionProps {
    tag: string,
    title: string,
    description: string,
    note: string,
    mainBtnText: string,
    secondBtnText : string,
}
const HeroSection: React.FC<HeroSectionProps> = ({tag , title , description , note , mainBtnText , secondBtnText }) => {
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
                    <span>{tag}</span>
                </a>

                <h1 className={styles.title}>
                    {title}<br />
                    <span className={styles.accent}>Your Financial Life</span>
                </h1>

                <p className={styles.tagline}>
                    {description }
                </p>

                <div className={styles.actions}>
                    <a href="#" className={styles.btnPrimary}>{mainBtnText}</a>
                    <a href="#" className={styles.btnSecondary}>
                        <span className={styles.playIcon}>▶</span>{secondBtnText}
                    </a>
                </div>

                <div className={styles.trust}>
                    <div className={styles.avatarStack}>
                        {colors.map((c, i) => (
                            <div key={i} className={styles.avatar} style={{ background: c, zIndex: 4 - i }} />
                        ))}
                    </div>
                    <span className={styles.trustText}>
                        <strong>120,000+</strong>{note}
                    </span>
                </div>

                <div className={styles.mockup} aria-hidden="true">
                    <div className={styles.mockupBar}>
                        <span className={styles.dot} style={{ background: "#ef4444" }} />
                        <span className={styles.dot} style={{ background: "#f59e0b" }} />
                        <span className={styles.dot} style={{ background: "#10b981" }} />
                        <span className={styles.mockupUrl}>app.FinanceCore.io/dashboard</span>
                    </div>
                    <div className={styles.mockupBody}>
                        <div className={styles.mockupSidebar}>
                            {navItems.map(item => (
                                <div key={item} className={`${styles.mockupNavItem} ${item === "Dashboard" ? styles.mockupNavActive : ""}`}>{item}</div>
                            ))}
                        </div>
                        <div className={styles.mockupContent}>
                            <div className={styles.mockupCards}>
                                {dashboardData.map(card => (
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
                                {barHeight.map((h, i) => (
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
