import React, { useEffect, useRef } from "react";
import styles from "./CtaSection.module.css";

interface CtaSectionProps {
    title: string,
    para: string,
    mainBtnMsg: string,
    secondBtnMsg: string,
}


const CtaSection: React.FC<CtaSectionProps> = ({title , para , mainBtnMsg , secondBtnMsg}) => {
    const ref = useRef<HTMLDivElement>(null);

    useEffect(() => {
        const el = ref.current;
        if (!el) return;
        const observer = new IntersectionObserver(
            ([entry]) => { if (entry.isIntersecting) { el.classList.add(styles.visible); observer.disconnect(); } },
            { threshold: 0.15 }
        );
        observer.observe(el);
        return () => observer.disconnect();
    }, []);

    return (
        <section className={styles.wrapper} ref={ref}>
            <div className={styles.card}>
                <div className={styles.blobLeft} aria-hidden="true" />
                <div className={styles.blobRight} aria-hidden="true" />
                <div className={styles.inner}>
                    <h2 className={styles.title}>
                        {title}<br />Starts Right Now
                    </h2>
                    <p className={styles.subtitle}>
                        {para}
                    </p>
                    <div className={styles.actions}>
                        <a href="#" className={styles.btnPrimary}>{mainBtnMsg}</a>
                        <a href="#" className={styles.btnSecondary}>{secondBtnMsg}</a>
                    </div>
                </div>
            </div>
        </section>
    );
};

export default CtaSection;