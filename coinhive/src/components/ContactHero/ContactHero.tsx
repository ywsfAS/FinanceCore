import React, { useEffect, useRef } from "react";
import styles from "./ContactHero.module.css";
 
const ContactHero: React.FC = () => {
  const heroRef = useRef<HTMLDivElement>(null);
 
  useEffect(() => {
    const el = heroRef.current;
    if (el) requestAnimationFrame(() => el.classList.add(styles.visible));
  }, []);
 
  return (
    <section className={styles.hero} ref={heroRef}>
      <div className={styles.gridOverlay} aria-hidden="true" />
      <div className={styles.gradientBlob} aria-hidden="true" />
      <div className={styles.inner}>
        <span className={styles.eyebrow}>Get In Touch</span>
        <h1 className={styles.title}>
          We're Here to <span className={styles.accent}>Help You</span>
        </h1>
        <p className={styles.tagline}>
          Whether you have a question about your account, need help with a
          feature, or want to explore a partnership — our team responds within
          one business day.
        </p>
        <div className={styles.badges}>
          <div className={styles.badge}>
            <span className={styles.badgeIcon}>⚡</span>
            <span>Avg. response under 4h</span>
          </div>
          <div className={styles.badge}>
            <span className={styles.badgeIcon}>🌍</span>
            <span>Support in 12 languages</span>
          </div>
          <div className={styles.badge}>
            <span className={styles.badgeIcon}>🔒</span>
            <span>Secure & confidential</span>
          </div>
        </div>
      </div>
    </section>
  );
};
 
export default ContactHero;