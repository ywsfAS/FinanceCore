import styles from "./MobileMenu.module.css";

export interface HamburgerProps {
    open: boolean;
    onClick: () => void;
}
export default function Hamburger({ open, onClick } : HamburgerProps) {
    return (
        <button
            className={styles.hamburger}
            onClick={onClick}
            aria-label={open ? "Close menu" : "Open menu"}
            aria-expanded={open}
        >
            <span className={styles.bar} />
            <span className={styles.bar} />
            <span className={styles.bar} />
        </button>
    );
}