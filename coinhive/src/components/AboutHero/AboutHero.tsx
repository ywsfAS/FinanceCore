import React, { useEffect, useRef } from "react";
import styles from "./AboutHero.module.css";

// TODO : create a variants and costume props !
const AboutHero: React.FC = () => {
    const heroRef = useRef<HTMLDivElement>(null);

    useEffect(() => {
        const el = heroRef.current;
        if (el) {
            requestAnimationFrame(() => el.classList.add(styles.visible));
        }
    }, []);

    return (
        <section className={styles.hero} ref={heroRef}>
            <div className={styles.gridOverlay} aria-hidden="true" />
            <div className={styles.gradientBlob} aria-hidden="true" />

            <div className={styles.inner}>
                <span className={styles.eyebrow}>Our Story</span>
                <h1 className={styles.title}>
                    Finance, Built Around
                    <br />
                    <span className={styles.accent}>Your Future</span>
                </h1>
                <p className={styles.tagline}>
                    We believe every financial decision deserves clarity, confidence, and
                    the tools to act on it. We build software that puts you in control —
                    of your goals, your growth, and your tomorrow.
                </p>
                <div className={styles.stats}>
                    <div className={styles.stat}>
                        <span className={styles.statNumber}>120K+</span>
                        <span className={styles.statLabel}>Active Users</span>
                    </div>
                    <div className={styles.statDivider} />
                    <div className={styles.stat}>
                        <span className={styles.statNumber}>$2.4B</span>
                        <span className={styles.statLabel}>Assets Tracked</span>
                    </div>
                    <div className={styles.statDivider} />
                    <div className={styles.stat}>
                        <span className={styles.statNumber}>99.9%</span>
                        <span className={styles.statLabel}>Uptime</span>
                    </div>
                </div>
            </div>
        </section>
    );
};

export default AboutHero;