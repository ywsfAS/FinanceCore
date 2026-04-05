import React from "react";
import styles from "./Footer.module.css";

const Footer: React.FC = () => {
    return (
        <footer className={styles.footer}>
            <div className={styles.inner}>
                <div className={styles.brand}>
                    <span className={styles.logo}>Finance Core</span>
                    <p className={styles.tagline}>Smart finance tools for modern life.</p>
                </div>
                <div className={styles.links}>
                    <div className={styles.linkGroup}>
                        <span className={styles.groupTitle}>Product</span>
                        <a href="#">Dashboard</a>
                        <a href="#">Goals</a>
                        <a href="#">Analytics</a>
                        <a href="#">Integrations</a>
                    </div>
                    <div className={styles.linkGroup}>
                        <span className={styles.groupTitle}>Company</span>
                        <a href="#">About Us</a>
                        <a href="#">Careers</a>
                        <a href="#">Press</a>
                        <a href="#">Blog</a>
                    </div>
                    <div className={styles.linkGroup}>
                        <span className={styles.groupTitle}>Legal</span>
                        <a href="#">Privacy Policy</a>
                        <a href="#">Terms of Service</a>
                        <a href="#">Security</a>
                    </div>
                </div>
            </div>
            <div className={styles.bottom}>
                <span>© {new Date().getFullYear()} Finance Core. All rights reserved.</span>
                <span className={styles.badge}>🔒 SOC 2 Certified</span>
            </div>
        </footer>
    );
};

export default Footer;