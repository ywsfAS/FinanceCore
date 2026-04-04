import React from "react";
import HeroSection from "../../components/HeroSection/HeroSection";
import LogoSection from "../../components/LogoSection/LogoSection";
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

        </main>
    );
};

export default Landing;