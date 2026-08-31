import React from "react";
import HeroSection from "../../components/HeroSection/HeroSection";
import FeaturesSection from "../../components/FeaturesSection/FeaturesSection";
import TestimonialsSection from "../../components/TestimonialsSection/TestimonialsSection";
import CtaSection from "../../components/Ctasection/CtaSection";
import HowItWorks from "../../components/Howitworks/HowItWorks";
import styles from "./Landing.module.css";
import ActiviyInsight from "../../components/ActivityInsights/ActivityInsight";

const Landing: React.FC = () => {
    return (
        <main className={styles.page}>
            {/* Hero */}
            <HeroSection
                title="Take Command of Your Financial Life"
                description="FinanceCore unifies your accounts, budgets, goals, and investments into one intelligent dashboard so you don’t just track your money, you understand it."
                mainBtnText="Get Started"
                secondBtnText=" Watch Demo"

            />
            {/* Features */}
            <div className={styles.container}>
                <FeaturesSection
                    title="Why Choose FinanceCore?"
                    description="From budgeting and expense tracking to savings goals and financial analytics, FinanceCore provides everything you need to organize your finances, make informed decisions, and build a stronger financial future."
                />
            </div>

            <ActiviyInsight title="Activity Insights" description="See what's happening across your system in real time. Track actions, monitor background operations, and stay updated on important events as they unfold." />
            {/* How it works */}

            <div className={styles.container}>
                <HowItWorks
                    title="Up and Running in Minutes"
                    subtitle="No financial expertise required. FinanceCore guides you from signup to full clarity in four simple steps."
                />
            </div>

            {/* Testimonials */}
            <div className={styles.container}>
                <TestimonialsSection />
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