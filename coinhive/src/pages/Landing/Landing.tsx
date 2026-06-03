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
                title="Take Full Control of"
                description="FinanceCore brings your accounts, budgets, goals, and investments into
                    one secure dashboard so you always know where you stand and where
                    you're headed."
                mainBtnText="Start for Free"
                secondBtnText=" Watch Demo"

            />

            {/* Social proof logos */}
            <div className={styles.container}>
                <LogoSection />
            </div>

            {/* Features */}
            <div className={styles.container}>
                <FeaturesSection
                    title="Built for the Way You Actually Live"
                    description="No more juggling five apps. FinanceCore handles every aspect of your
                    financial life in one clean, secure platform."
                />
            </div>

            {/* How it works */}
            
                <div className={styles.container}>
                    <HowItWorks
                        title="Up and Running in Minutes"
                        subtitle="No financial expertise required. FinanceCore guides you from signup to full clarity in four simple steps."
                    />
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
                    para="Join 120,000+ users who've taken control of their money with FinanceCore. Free forever. No credit card required."
                    mainBtnMsg="Create Free Account"
                    secondBtnMsg="Talk to Sales"
                />
            </div>

        </main>
    );
};

export default Landing;