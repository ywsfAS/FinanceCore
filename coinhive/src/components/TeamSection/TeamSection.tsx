import React, { useEffect, useRef } from "react";
import styles from "./TeamSection.module.css";

interface TeamMember {
    name: string;
    role: string;
    bio: string;
    initials: string;
    color: string;
}

const team: TeamMember[] = [
    {
        name: "Sarah Ellison",
        role: "CEO & Co-Founder",
        bio: "Former Goldman Sachs analyst with 12 years in fintech. Sarah leads with a vision to democratize personal finance.",
        initials: "SE",
        color: "#10b981",
    },
    {
        name: "Marcus Chen",
        role: "CTO & Co-Founder",
        bio: "Ex-Stripe engineer who architected payment systems at scale. Marcus ensures our platform is fast, secure, and reliable.",
        initials: "MC",
        color: "#3b82f6",
    },
    {
        name: "Amira Osei",
        role: "Head of Product",
        bio: "UX-driven product leader who has shipped tools used by millions at Intuit. Amira obsesses over every user interaction.",
        initials: "AO",
        color: "#8b5cf6",
    },
    {
        name: "Daniel Rousseau",
        role: "Head of Security",
        bio: "Certified ethical hacker and former cybersecurity consultant. Daniel leads our commitment to bank-grade data protection.",
        initials: "DR",
        color: "#f59e0b",
    },
    {
        name: "Lena Park",
        role: "Lead Data Scientist",
        bio: "PhD in Financial Mathematics from MIT. Lena builds the intelligent models that power our personalized insights engine.",
        initials: "LP",
        color: "#ef4444",
    },
    {
        name: "James Mbeki",
        role: "Head of Growth",
        bio: "Growth strategist with a track record across N26 and Revolut. James connects users to the tools that change their lives.",
        initials: "JM",
        color: "#06b6d4",
    },
];

const TeamSection: React.FC = () => {
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
            { threshold: 0.1 }
        );

        observer.observe(el);
        return () => observer.disconnect();
    }, []);

    return (
        <section className={styles.wrapper} ref={ref}>
            <div className={styles.header}>
                <span className={styles.eyebrow}>The Team</span>
                <h2 className={styles.title}>People Behind the Platform</h2>
                <p className={styles.subtitle}>
                    A diverse team of engineers, designers, and finance experts united by
                    one mission: making financial clarity accessible to everyone.
                </p>
            </div>

            <div className={styles.grid}>
                {team.map((member, i) => (
                    <div
                        key={member.name}
                        className={styles.card}
                        style={{ transitionDelay: `${i * 80}ms` }}
                    >
                        <div
                            className={styles.avatar}
                            style={{ background: member.color + "18", color: member.color }}
                        >
                            {member.initials}
                        </div>
                        <div className={styles.cardBody}>
                            <h3 className={styles.name}>{member.name}</h3>
                            <span
                                className={styles.role}
                                style={{ color: member.color }}
                            >
                                {member.role}
                            </span>
                            <p className={styles.bio}>{member.bio}</p>
                        </div>
                    </div>
                ))}
            </div>
        </section>
    );
};

export default TeamSection;