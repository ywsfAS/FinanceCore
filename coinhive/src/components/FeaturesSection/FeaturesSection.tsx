// FeaturesSection.tsx
import React from "react";
import styles from "./FeaturesSection.module.css";
import {features} from "./Constants";

interface FeaturesSectionProps {
    title: string;
    description: string;
}

const FeaturesSection: React.FC<FeaturesSectionProps> = ({ title, description }) => {
    return (
        <section className={styles.wrapper} >
            <div className={styles.header}>
                <h2 className={styles.title}>{title}</h2>
                <p className={styles.subtitle}>{description}</p>
            </div>
            <div className={styles.grid}>
                {features.map((f) => (
                    <div
                        key={f.title}
                        className={styles.card}
                        >
                        <div
                            className={styles.icon}
                        >
                            {f.icon}
                        </div>
                        <h3 className={styles.cardTitle}>{f.title}</h3>
                        <p className={styles.cardDesc}>{f.description}</p>
                    </div>
                ))}
            </div>
        </section>
    );
};

export default FeaturesSection;