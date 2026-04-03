import React, { useEffect, useRef, useState } from "react";
import styles from "./FaqSection.module.css";
 
interface FaqItem {
  question: string;
  answer: string;
}
 
const faqs: FaqItem[] = [
  {
    question: "How quickly will I receive a response?",
    answer:
      "Our support team typically responds within 4 business hours. For Enterprise plan customers, we guarantee a 1-hour response SLA during business hours.",
  },
  {
    question: "Is my financial data safe when I contact support?",
    answer:
      "Absolutely. All communications are encrypted in transit and at rest. Our support agents follow strict data handling protocols and will never ask for your password or full account number.",
  },
  {
    question: "Can I schedule a call with the sales team?",
    answer:
      "Yes — select 'Partnership Inquiry' or 'Enterprise' in the subject dropdown and mention you'd like a call. We'll send you a calendar link within one business day.",
  },
  {
    question: "What's the best way to report a security vulnerability?",
    answer:
      "Please use the 'Security Concern' subject in the contact form, or email security@finvault.io directly. We take all reports seriously and operate a responsible disclosure policy.",
  },
  {
    question: "Do you offer support in languages other than English?",
    answer:
      "Yes — our team supports 12 languages including French, Spanish, Arabic, German, Portuguese, and Japanese. Mention your preferred language in your message.",
  },
];
 
const FaqSection: React.FC = () => {
  const ref = useRef<HTMLDivElement>(null);
  const [openIndex, setOpenIndex] = useState<number | null>(null);
 
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
        <span className={styles.eyebrow}>FAQ</span>
        <h2 className={styles.title}>Common Questions</h2>
        <p className={styles.subtitle}>
          Answers to the questions we hear most often. Can't find what you're
          looking for? Send us a message above.
        </p>
      </div>
 
      <div className={styles.list}>
        {faqs.map((faq, i) => (
          <div
            key={i}
            className={`${styles.item} ${openIndex === i ? styles.open : ""}`}
            style={{ transitionDelay: `${i * 60}ms` }}
          >
            <button
              className={styles.question}
              onClick={() => setOpenIndex(openIndex === i ? null : i)}
              aria-expanded={openIndex === i}
            >
              <span>{faq.question}</span>
              <span className={styles.chevron} aria-hidden="true">
                {openIndex === i ? "−" : "+"}
              </span>
            </button>
            <div className={styles.answerWrapper}>
              <p className={styles.answer}>{faq.answer}</p>
            </div>
          </div>
        ))}
      </div>
    </section>
  );
};
 
export default FaqSection;