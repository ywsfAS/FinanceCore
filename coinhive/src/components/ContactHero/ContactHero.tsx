import React, { useEffect, useRef } from "react";
import styles from "./ContactHero.module.css";

interface Badge {
    icon: string,
    description : string,
}
interface ContactHeroProps {
    tag: string,
    title: string,
    para: string,
    badge1: Badge,
    badge2: Badge,
    badge3 : Badge
}
const ContactHero: React.FC<ContactHeroProps> = ({tag , title , para , badge1 , badge2 , badge3 }) => {
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
              <span className={styles.eyebrow}>{tag}</span>
        <h1 className={styles.title}>
                  {title}<span className={styles.accent}>Help You</span>
        </h1>
        <p className={styles.tagline}>
         {para}
        </p>
        <div className={styles.badges}>
          <div className={styles.badge}>
            <span className={styles.badgeIcon}>{ badge1.icon}</span>
             <span>{badge1.description}</span>
          </div>
           <div className={styles.badge}>
              <span className={styles.badgeIcon}>{badge2.icon}</span>
              <span>{badge2.description}</span>
          </div>
          <div className={styles.badge}>
              <span className={styles.badgeIcon}>{badge3.icon}</span>
              <span>{badge3.description}</span>
          </div>
        </div>
      </div>
    </section>
  );
};
 
export default ContactHero;