import React, { useEffect, useRef } from "react";
import styles from "./ContactHero.module.css";

interface Badge {
    icon: string,
    description : string,
}
interface ContactHeroProps {
    title: string,
    para: string,
}
const ContactHero: React.FC<ContactHeroProps> = ({ title , para }) => {
  const heroRef = useRef<HTMLDivElement>(null);
 
  useEffect(() => {
    const el = heroRef.current;
    if (el) requestAnimationFrame(() => el.classList.add(styles.visible));
  }, []);
 
  return (
    <section className={styles.hero} ref={heroRef}>
      <div className={styles.inner}>
        <h1 className={styles.title}>
                  {title}<span className={styles.accent}>Help You</span>
        </h1>
        <p className={styles.tagline}>
         {para}
        </p>
      </div>
    </section>
  );
};
 
export default ContactHero;