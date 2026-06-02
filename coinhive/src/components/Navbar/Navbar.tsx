import React, { useEffect, useState } from "react";
import styles from "./Navbar.module.css";
import { useTheme } from "../../hooks/Theme/Theme";
import { Link } from "react-router-dom";
import Button from "../Button/Button";
import { Moon , Sun } from "lucide-react";

type NavPage = "home" | "about" | "contact" | "pricing" | "profile";

interface NavbarProps {
    activePage?: NavPage;
}

const navLinks: { label: string; href: string; page: NavPage }[] = [
    { label: "Product", href: "#", page: "home" },
    { label: "Profile", href: "/profile", page: "profile" },
    { label: "Pricing", href: "/pricing", page: "pricing" },
    { label: "About", href: "/about", page: "about" },
    { label: "Contact", href: "/contact", page: "contact" },
];

const Navbar: React.FC<NavbarProps> = ({ activePage = "home" }) => {
    const [scrolled, setScrolled] = useState(false);
    const [menuOpen, setMenuOpen] = useState(false);
    const [visible, setVisible] = useState(true);
    const [lastScrollY, setLastScrollY] = useState(0);

    const { theme , toggleTheme } = useTheme();

    useEffect(() => {
        const onScroll = () => {
            const y = window.scrollY;
            setScrolled(y > 12);
            setVisible(y < lastScrollY || y < 80);
            setLastScrollY(y);
        };
        window.addEventListener("scroll", onScroll, { passive: true });
        return () => window.removeEventListener("scroll", onScroll);
    }, [lastScrollY]);

    useEffect(() => {
        document.body.style.overflow = menuOpen ? "hidden" : "";
        return () => { document.body.style.overflow = ""; };
    }, [menuOpen]);

    return (
        <>
            <header
                className={[
                    styles.header,
                    theme === 'dark' ? styles.dark : "",
                    scrolled ? styles.scrolled : "",
                    !visible ? styles.hidden : "",
                    menuOpen ? styles.menuOpen : "",
                ].join(" ")}
            >
                <div className={styles.inner}>

                    <a href="/" className={styles.logo} aria-label="FinanceCore home">
                        <span className={styles.logoMark}>FC</span>
                        <span className={styles.logoText}>FinanceCore</span>
                    </a>

                    <nav className={styles.desktopNav} aria-label="Main navigation">
                        {navLinks.map((link) => (
                            <Link
                                key={link.page}
                                to={link.href}
                                className={[
                                    styles.navLink,
                                    activePage === link.page ? styles.navLinkActive : "",
                                ].join(" ")}
                            >
                                {link.label}
                                {activePage === link.page && (
                                    <span className={styles.activeDot} aria-hidden="true" />
                                )}
                            </Link>
                        ))}
                    </nav>

                    <div className={styles.desktopActions}>
                        <Link to="/login" className={styles.loginBtn}>LogIn</Link>
                        <Link to="/register" className={styles.ctaBtn}>Get Started Free</Link>
                        <Button onClick={toggleTheme} variant="purple" size="small">{theme === 'dark' ? <Moon size={20} /> : <Sun size={20} /> }</Button>
                    </div>

                    <button
                        className={styles.hamburger}
                        onClick={() => setMenuOpen((o) => !o)}
                        aria-label={menuOpen ? "Close menu" : "Open menu"}
                        aria-expanded={menuOpen}
                    >
                        <span className={styles.bar} />
                        <span className={styles.bar} />
                        <span className={styles.bar} />
                    </button>
                </div>
            </header>

            <div
                className={[styles.mobileDrawer, menuOpen ? styles.drawerOpen : ""].join(" ")}
                aria-hidden={!menuOpen}
            >
                <nav className={styles.mobileNav} aria-label="Mobile navigation">
                    {navLinks.map((link) => (
                        <a
                            key={link.page}
                            href={link.href}
                            className={[
                                styles.mobileLink,
                                activePage === link.page ? styles.mobileLinkActive : "",
                            ].join(" ")}
                            onClick={() => setMenuOpen(false)}
                        >
                            {link.label}
                        </a>
                    ))}
                </nav>

                <div className={styles.mobileActions}>
                    <a href="/login" className={styles.mobileLogin} onClick={() => setMenuOpen(false)}>Log In</a>
                    <a href="/register" className={styles.mobileCta} onClick={() => setMenuOpen(false)}>Get Started Free</a>

                </div>

                <div className={styles.mobileFooter}>
                    <span>SOC 2 Certified</span>
                    <span>·</span>
                    <span>99.9% Uptime</span>
                </div>
            </div>

            {menuOpen && (
                <div
                    className={styles.backdrop}
                    onClick={() => setMenuOpen(false)}
                    aria-hidden="true"
                />
            )}
        </>
    );
};

export default Navbar;