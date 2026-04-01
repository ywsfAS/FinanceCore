import React, { useEffect, useRef } from "react";
import styles from "./AboutSection.module.css";

interface AboutSectionProps {
    title: string;
    text: string;
    image?: string;
    imageAlt?: string;
    reverse?: boolean;
    tag?: string;
}

const AboutSection: React.FC<AboutSectionProps> = ({
    title,
    text,
    image,
    imageAlt = "",
    reverse = false,
    tag,
}) => {
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
            { threshold: 0.15 }
        );

        observer.observe(el);
        return () => observer.disconnect();
    }, []);

    return (
        <div
            ref={ref}
            className={`${styles.section} ${reverse ? styles.reverse : ""}`}
        >
            <div className={styles.textBlock}>
                {tag && <span className={styles.tag}>{tag}</span>}
                <h2 className={styles.title}>{title}</h2>
                <p className={styles.text}>{text}</p>
                <div className={styles.line} />
            </div>

            {image && (
                <div className={styles.imageBlock}>
                    <img src={image} alt={imageAlt} className={styles.image} />
                    <div className={styles.imageBorder} />
                </div>
            )}

            {!image && (
                <div className={styles.illustrationBlock}>
                    <div className={styles.illustrationInner} aria-hidden="true">
                        <div className={styles.circle1} />
                        <div className={styles.circle2} />
                        <div className={styles.dot} />
                    </div>
                </div>
            )}
        </div>
    );
};

export default AboutSection;