import React, { useEffect, useRef } from "react";
import styles from "./ContactInfo.module.css";

import {
    Mail,
    MessageCircle,
    Phone,
    Building2,
    ArrowRight,
} from "lucide-react";

interface Channel {
    icon: React.ReactNode;
    label: string;
    value: string;
    detail: string;
    color: string;
}

interface ContactInfoProps {
    tag: string;
    title: string;
    description: string;
}

const channels: Channel[] = [
    {
        icon: <Mail size={20} />,
        label: "Email Us",
        value: "support@finvault.io",
        detail: "We reply within 4 business hours",
        color: "#10b981",
    },
    {
        icon: <MessageCircle size={20} />,
        label: "Live Chat",
        value: "Available in-app",
        detail: "Mon–Fri, 9am–6pm CET",
        color: "#3b82f6",
    },
    {
        icon: <Phone size={20} />,
        label: "Call Us",
        value: "+1 (800) 392-4455",
        detail: "Enterprise & Pro plans only",
        color: "#8b5cf6",
    },
];

const offices = [
    {
        city: "San Francisco",
        address: "101 Market St, Suite 800, CA 94105",
    },
    {
        city: "London",
        address: "30 St Mary Axe, EC3A 8BF",
    },
    {
        city: "Dubai",
        address: "DIFC, Gate Avenue, Level 5",
    },
];

const ContactInfo: React.FC<ContactInfoProps> = ({
    title,
    tag,
    description,
}) => {
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
        <div className={styles.wrapper} ref={ref}>
            <div className={styles.header}>
                <span className={styles.eyebrow}>{tag}</span>

                <h2 className={styles.title}>{title}</h2>

                <p className={styles.subtitle}>{description}</p>
            </div>

            <div className={styles.channels}>
                {channels.map((ch, i) => (
                    <div
                        key={ch.label}
                        className={styles.channelCard}
                        style={{ transitionDelay: `${i * 80}ms` }}
                    >
                        <div
                            className={styles.channelIcon}
                            style={{
                                background: `${ch.color}18`,
                                color: ch.color,
                            }}
                        >
                            {ch.icon}
                        </div>

                        <div className={styles.channelBody}>
                            <span className={styles.channelLabel}>
                                {ch.label}
                            </span>

                            <span className={styles.channelValue}>
                                {ch.value}
                            </span>

                            <span className={styles.channelDetail}>
                                {ch.detail}
                            </span>
                        </div>
                    </div>
                ))}
            </div>

            <div className={styles.officesBlock}>
                <span className={styles.officesTitle}>
                    Our Offices
                </span>

                <div className={styles.offices}>
                    {offices.map((office) => (
                        <div
                            key={office.city}
                            className={styles.officeCard}
                        >
                            <div className={styles.officeIcon}>
                                <Building2 size={18} />
                            </div>

                            <div>
                                <span className={styles.officeCity}>
                                    {office.city}
                                </span>

                                <span className={styles.officeAddress}>
                                    {office.address}
                                </span>
                            </div>
                        </div>
                    ))}
                </div>
            </div>

            <div className={styles.faqNote}>
                <div>
                    <span className={styles.faqText}>
                        Looking for quick answers?
                    </span>

                    <a href="#" className={styles.faqLink}>
                        Browse our Help Center
                        <ArrowRight size={15} />
                    </a>

                    <span className={styles.faqSub}>
                        500+ articles covering every feature and
                        common question.
                    </span>
                </div>
            </div>
        </div>
    );
};

export default ContactInfo;