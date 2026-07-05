import styles from "./Footer.module.css";
import { DEFAULT } from "./constants";
import type { FooterProps } from "./types";

const Footer = ({
    logo = DEFAULT.logo,
    items = DEFAULT.items,
    tagline = DEFAULT.tagline,
    copyright = DEFAULT.copyright

}: FooterProps) => {
    console.log(DEFAULT)
    return (
        <footer className={styles.footer}>
            <div className={styles.inner}>
                <div className={styles.brand}>
                    <span className={styles.logo}>{logo}</span>
                    <p className={styles.tagline}>{tagline}</p>
                </div>
                <div className={styles.links}>
                    {items.map(item => (
                        <div className={styles.linkGroup}>
                            <span className={styles.groupTitle}>{item.groupName}</span>
                            {item.groupOptions.map(option =>
                                <a href="#">
                                    {option}</a>
                            )}
                        </div>
                    ))}
                </div>
            </div>
            <div className={styles.bottom}>
                <span>© {new Date().getFullYear()} {copyright} </span>
            </div>
        </footer>
    );
};

export default Footer;