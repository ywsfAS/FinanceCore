import React, { useEffect, useRef } from "react";
import styles from "./ContactInfo.module.css";
 
interface Channel {
  icon: string;
  label: string;
  value: string;
  detail: string;
  color: string;
}
interface ContactInfo {
    tag: string,
    title: string,
    description : string
} 
const channels: Channel[] = [
  {
    icon: "✉️",
    label: "Email Us",
    value: "support@finvault.io",
    detail: "We reply within 4 business hours",
    color: "#10b981",
  },
  {
    icon: "💬",
    label: "Live Chat",
    value: "Available in-app",
    detail: "Mon–Fri, 9am–6pm CET",
    color: "#3b82f6",
  },
  {
    icon: "📞",
    label: "Call Us",
    value: "+1 (800) 392-4455",
    detail: "Enterprise & Pro plans only",
    color: "#8b5cf6",
  },
];
 
const offices = [
  { city: "San Francisco", address: "101 Market St, Suite 800, CA 94105", flag: "🇺🇸" },
  { city: "London", address: "30 St Mary Axe, EC3A 8BF", flag: "🇬🇧" },
  { city: "Dubai", address: "DIFC, Gate Avenue, Level 5", flag: "🇦🇪" },
];
 
const ContactInfo: React.FC<ContactInfo> = ({title , tag , description }) => {
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
         <p className={styles.subtitle}>
          {description }
        </p>
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
              style={{ background: ch.color + "18", color: ch.color }}
            >
              {ch.icon}
            </div>
            <div className={styles.channelBody}>
              <span className={styles.channelLabel}>{ch.label}</span>
              <span className={styles.channelValue} style={{ color: ch.color }}>
                {ch.value}
              </span>
              <span className={styles.channelDetail}>{ch.detail}</span>
            </div>
          </div>
        ))}
      </div>
 
      <div className={styles.officesBlock}>
        <span className={styles.officesTitle}>Our Offices</span>
        <div className={styles.offices}>
          {offices.map((o) => (
            <div key={o.city} className={styles.officeCard}>
              <span className={styles.officeFlag}>{o.flag}</span>
              <div>
                <span className={styles.officeCity}>{o.city}</span>
                <span className={styles.officeAddress}>{o.address}</span>
              </div>
            </div>
          ))}
        </div>
      </div>
 
      <div className={styles.faqNote}>
        <span className={styles.faqIcon}>💡</span>
        <div>
          <span className={styles.faqText}>
            Looking for quick answers?{" "}
            <a href="#" className={styles.faqLink}>
              Browse our Help Center →
            </a>
          </span>
          <span className={styles.faqSub}>
            500+ articles covering every feature and common question.
          </span>
        </div>
      </div>
    </div>
  );
};
 
export default ContactInfo;