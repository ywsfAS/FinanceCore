import React, { useEffect, useRef } from "react";
import styles from "./AboutHero.module.css";


interface Stat {
    name: string,
    value : string,
}
interface AboutHeroProps {
    title: string,
    tag: string,
    para: string,
    Stat1: Stat,
    Stat2: Stat,
    Stat3 : Stat,

}
const AboutHero: React.FC<AboutHeroProps> = ({title , tag , para , Stat1 , Stat2 , Stat3}) => {
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
                <span className={styles.eyebrow}>{tag}</span>
                <h1 className={styles.title}>
                    {title}
                    <br />
                    <span className={styles.accent}>Your Future</span>
                </h1>
                <p className={styles.tagline}>
                    {para}

                </p>
                <div className={styles.stats}>
                    <div className={styles.stat}>
                        <span className={styles.statNumber}>{Stat1.value}</span>
                        <span className={styles.statLabel}>{Stat1.name}</span>
                    </div>
                    <div className={styles.statDivider} />
                    <div className={styles.stat}>
                        <span className={styles.statNumber}>{Stat2.value}</span>
                        <span className={styles.statLabel}>{Stat2.name}</span>
                    </div>
                    <div className={styles.statDivider} />
                    <div className={styles.stat}>
                        <span className={styles.statNumber}>{Stat3.value}</span>
                        <span className={styles.statLabel}>{Stat3.name}</span>
                    </div>
                </div>
            </div>
        </section>
    );
};

export default AboutHero;