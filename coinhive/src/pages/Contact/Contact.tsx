import React from "react";
import ContactHero from "../../components/ContactHero/ContactHero";
import ContactForm from "../../components/ContactForm/ContactForm";
import ContactInfo from "../../components/ContactInfo/ContactInfo";
import FaqSection from "../../components/Faqsection/FaqSection";
import styles from "./Contact.module.css";
 
const Contact: React.FC = () => {
  return (
    <main className={styles.page}>
      {/* Hero */}
      <ContactHero />
 
      {/* Form + Info side by side */}
      <div className={styles.mainGrid}>
        <ContactForm />
        <ContactInfo />
      </div>
 
      {/* FAQ */}
      <hr className={styles.divider} />
      <div className={styles.faqWrapper}>
        <FaqSection />
      </div>
    </main>
  );
};
 
export default Contact;