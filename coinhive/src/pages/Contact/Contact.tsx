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
      <ContactHero
              tag="Get In Touch"
              title="We're Here to "
              para="Whether you have a question about your account, need help with a
          feature, or want to explore a partnership — our team responds within
          one business day."
              badge1={{ icon: "⚡", description: "Avg. response under 4h"}}
              badge2={{ icon: "🌍", description: "Support in 12 languages"}}
              badge3={{ icon: "🔒", description: "Secure & confidential"}}
          />
 
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