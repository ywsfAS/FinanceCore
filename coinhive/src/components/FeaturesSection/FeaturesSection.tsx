// FeaturesSection.tsx
import React from "react";
import styles from "./FeaturesSection.module.css";
import { features, waterConfig, animationConfig } from "./Constants";
import { motion } from 'motion/react';

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
                {features.map((f, index) => (
                    <motion.div
                        {...animationConfig(index, f.direction)}
                        key={f.title}
                        className={styles.card}
                    >
                        <motion.div
                            className={styles.water}
                            {...waterConfig(f.waterLevel)}
                        />
                        <div
                            className={styles.icon}
                        >
                            {f.icon}
                        </div>
                        <h3 className={styles.cardTitle}>{f.title}</h3>
                        <p className={styles.cardDesc}>{f.description}</p>
                    </motion.div>
                ))}
            </div>
        </section>
    );
};

export default FeaturesSection;