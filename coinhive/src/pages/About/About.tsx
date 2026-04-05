import React from "react";
import AboutHero from "../../components/AboutHero/AboutHero";
import AboutSection from "../../components/AboutSection/AboutSection";
import TeamSection from "../../components/TeamSection/TeamSection";
import ValuesSection from "../../components/ValueSection/ValueSection";
import styles from "./About.module.css";

const About: React.FC = () => {
    return (
        <main className={styles.page}>

            {/* Hero */}
            <AboutHero
                tag="Our Story"
                title="Finance, Built Around"
                para="We believe every financial decision deserves clarity, confidence, and
                    the tools to act on it. We build software that puts you in control
                    of your goals, your growth, and your tomorrow."
                Stat1={{ name: "Active Users", value: "120K+"}}
                Stat2={{ name: "Assets Tracked", value: "$2.4B"}}
                Stat3={{ name: "Uptime", value: "99.9%"}}

            />

            {/* Mission / Vision / Story sections */}
            <div className={styles.sections}>
                <AboutSection
                    tag="Our Mission"
                    title="Empowering Smarter Financial Decisions"
                    text="We started Finance Core with a simple belief: everyone deserves the financial tools that were once reserved for the wealthy. Our platform brings institutional-grade analytics, goal tracking, and portfolio insights to individuals, startups, and families — without the complexity or steep fees."
                />
                <AboutSection
                    tag="Our Vision"
                    title="A World Where Money Isn't a Mystery"
                    text="We envision a future where financial stress is replaced by financial confidence. Where budgeting is effortless, investing is approachable, and every person can see exactly where they stand and where they're headed — in real time."
                    reverse
                />
                <AboutSection
                    tag="Our Story"
                    title="From a Spreadsheet to a Platform Trusted by Thousands"
                    text="It started in 2019 when our founders, frustrated with fragmented tools and opaque bank dashboards, built a single spreadsheet to track their finances. That spreadsheet grew into a prototype. The prototype became FinVault — now used by over 120,000 people across 34 countries."
                />
            </div>
            {/* Values */}
            <div className={styles.valuesWrapper}>
                <ValuesSection />
            </div>

            {/* Team */}
            <div className={styles.teamWrapper}>
                <TeamSection />
            </div>

        </main>
    );
};

export default About;