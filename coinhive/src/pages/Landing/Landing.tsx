import React from "react";
import HeroSection from "../../components/HeroSection/HeroSection";
import LogoSection from "../../components/LogoSection/LogoSection";
import FeaturesSection from "../../components/FeaturesSection/FeaturesSection";
import TestimonialsSection from  "../../components/TestimonialsSection/TestimonialsSection";
import PricingSection from "../../components/Pricingsection/PricingSection";
import CtaSection from "../../components/Ctasection/CtaSection";
import  HowItWorks from "../../components/Howitworks/HowItWorks";
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

            {/* Testimonials */}
            <TestimonialsSection />

            {/* Pricing */}
            <div className={styles.container}>
                <PricingSection />
            </div>

            {/* CTA */}
            <div className={styles.container}>
                <CtaSection />
            </div>

        </main>
    );
};

export default Landing;