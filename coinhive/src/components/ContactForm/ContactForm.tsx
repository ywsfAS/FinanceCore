import React, { useEffect, useRef, useState } from "react";
import styles from "./ContactForm.module.css";
 
type FormState = "idle" | "submitting" | "success" | "error";
 
interface FormData {
  name: string;
  email: string;
  subject: string;
  message: string;
}
 
const subjects = [
  "Account & Billing",
  "Technical Support",
  "Feature Request",
  "Partnership Inquiry",
  "Security Concern",
  "Other",
];
 
const ContactForm: React.FC = () => {
  const ref = useRef<HTMLDivElement>(null);
  const [formState, setFormState] = useState<FormState>("idle");
  const [formData, setFormData] = useState<FormData>({
    name: "",
    email: "",
    subject: "",
    message: "",
  });
  const [touched, setTouched] = useState<Record<string, boolean>>({});
 
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
 
  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>
  ) => {
    setFormData((prev) => ({ ...prev, [e.target.name]: e.target.value }));
  };
 
  const handleBlur = (e: React.FocusEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    setTouched((prev) => ({ ...prev, [e.target.name]: true }));
  };
 
  const isValid = (field: keyof FormData) => {
    if (!touched[field]) return true;
    if (field === "email") return /\S+@\S+\.\S+/.test(formData.email);
    return formData[field].trim().length > 0;
  };
 
  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    setTouched({ name: true, email: true, subject: true, message: true });
    const allValid =
      formData.name.trim() &&
      /\S+@\S+\.\S+/.test(formData.email) &&
      formData.subject &&
      formData.message.trim();
    if (!allValid) return;
 
    setFormState("submitting");
    setTimeout(() => {
      setFormState("success");
    }, 1600);
  };
 
  return (
    <div className={styles.wrapper} ref={ref}>
      <div className={styles.header}>
        <span className={styles.eyebrow}>Send a Message</span>
        <h2 className={styles.title}>Tell Us How We Can Help</h2>
        <p className={styles.subtitle}>
          Fill out the form and one of our team members will get back to you
          shortly.
        </p>
      </div>
 
      {formState === "success" ? (
        <div className={styles.successCard}>
          <div className={styles.successIcon}>✓</div>
          <h3 className={styles.successTitle}>Message Sent!</h3>
          <p className={styles.successText}>
            Thanks for reaching out. We'll get back to you within one business
            day.
          </p>
          <button
            className={styles.resetBtn}
            onClick={() => {
              setFormState("idle");
              setFormData({ name: "", email: "", subject: "", message: "" });
              setTouched({});
            }}
          >
            Send Another Message
          </button>
        </div>
      ) : (
        <form className={styles.form} onSubmit={handleSubmit} noValidate>
          <div className={styles.row}>
            <div className={styles.field}>
              <label className={styles.label} htmlFor="name">
                Full Name
              </label>
              <input
                id="name"
                name="name"
                type="text"
                placeholder="John Doe"
                value={formData.name}
                onChange={handleChange}
                onBlur={handleBlur}
                className={`${styles.input} ${!isValid("name") ? styles.inputError : ""}`}
              />
              {!isValid("name") && (
                <span className={styles.error}>Name is required</span>
              )}
            </div>
            <div className={styles.field}>
              <label className={styles.label} htmlFor="email">
                Email Address
              </label>
              <input
                id="email"
                name="email"
                type="email"
                placeholder="john@example.com"
                value={formData.email}
                onChange={handleChange}
                onBlur={handleBlur}
                className={`${styles.input} ${!isValid("email") ? styles.inputError : ""}`}
              />
              {!isValid("email") && (
                <span className={styles.error}>Valid email is required</span>
              )}
            </div>
          </div>
 
          <div className={styles.field}>
            <label className={styles.label} htmlFor="subject">
              Subject
            </label>
            <select
              id="subject"
              name="subject"
              value={formData.subject}
              onChange={handleChange}
              onBlur={handleBlur}
              className={`${styles.input} ${styles.select} ${!isValid("subject") ? styles.inputError : ""}`}
            >
              <option value="">Select a topic…</option>
              {subjects.map((s) => (
                <option key={s} value={s}>
                  {s}
                </option>
              ))}
            </select>
            {!isValid("subject") && (
              <span className={styles.error}>Please select a subject</span>
            )}
          </div>
 
          <div className={styles.field}>
            <label className={styles.label} htmlFor="message">
              Message
            </label>
            <textarea
              id="message"
              name="message"
              rows={5}
              placeholder="Describe your question or issue in detail…"
              value={formData.message}
              onChange={handleChange}
              onBlur={handleBlur}
              className={`${styles.input} ${styles.textarea} ${!isValid("message") ? styles.inputError : ""}`}
            />
            {!isValid("message") && (
              <span className={styles.error}>Message is required</span>
            )}
          </div>
 
          <button
            type="submit"
            className={styles.submit}
            disabled={formState === "submitting"}
          >
            {formState === "submitting" ? (
              <span className={styles.spinner} />
            ) : (
              "Send Message →"
            )}
          </button>
        </form>
      )}
    </div>
  );
};
 
export default ContactForm;