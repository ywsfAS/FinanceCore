import React from "react";
import HeroSection from "../../components/HeroSection/HeroSection";
import LogoSection from "../../components/LogoSection/LogoSection";
import FeaturesSection from "../../components/FeaturesSection/FeaturesSection";
import HowItWorks from "../../components/Howitworks/HowItWorks";
import styles from "./Landing.module.css";

const Landing: React.FC = () => {
    return (
        <main className={styles.page}>
            {/* Hero */}
            <HeroSection />

            {/* Social proof logos */}
            <div className={styles.container}>
                <LogoSection />
            </div>

            {/* Features */}
            <div className={styles.container}>
                <FeaturesSection />
            </div>

            {/* How it works */}
            <div className={styles.altBg}>
                <div className={styles.container}>
                    <HowItWorks />
                </div>
            </div>

        </main>
    );
};

export default Landing;