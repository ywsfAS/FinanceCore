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
            <HeroSection
                tag="AI-powered spending insights are live →"
                title="Take Full Control of"
                description="FinVault brings your accounts, budgets, goals, and investments into
                    one secure dashboard — so you always know where you stand and where
                    you're headed."
                mainBtnText="Start for Free →"
                secondBtnText=" Watch Demo"
                note="Trusted users across 34 countries"

            />

            {/* Social proof logos */}
            <div className={styles.container}>
                <LogoSection />
            </div>

            {/* Features */}
            <div className={styles.container}>
                <FeaturesSection
                    tag="Everything You Need"
                    title="Built for the Way You Actually Live"
                    description="No more juggling five apps. FinVault handles every aspect of your
                    financial life in one clean, secure platform."
                />
            </div>

            {/* How it works */}
            <div className={styles.altBg}>
                <div className={styles.container}>
                    <HowItWorks
                        title="Up and Running in Minutes"
                        subtitle="No financial expertise required. Finance Core guides you from signup to full clarity in four simple steps."
                    />
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
                <CtaSection
                    title="Your Financial Future"
                    tag="Get Started Today"
                    para="Join 120,000+ users who've taken control of their money with FinVault. Free forever. No credit card required."
                    mainBtnMsg="Create Free Account →"
                    secondBtnMsg="Talk to Sales"
                />
            </div>

        </main>
    );
};

export default Landing;