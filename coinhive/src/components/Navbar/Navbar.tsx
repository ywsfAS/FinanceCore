import React, { useEffect, useState } from "react";
import styles from "./Navbar.module.css";

type NavPage = "home" | "about" | "contact" | "pricing" | "profile";

interface NavbarProps {
    activePage?: NavPage;
}

const navLinks: { label: string; href: string; page: NavPage }[] = [
    { label: "Product", href: "#", page: "home" },
    { label: "Profile", href: "#", page: "features" },
    { label: "Pricing", href: "#", page: "pricing" },
    { label: "About", href: "#", page: "about" },
    { label: "Contact", href: "#", page: "contact" },
];

const Navbar: React.FC<NavbarProps> = ({ activePage = "home" }) => {
    const [scrolled, setScrolled] = useState(false);
    const [menuOpen, setMenuOpen] = useState(false);
    const [visible, setVisible] = useState(true);
    const [lastScrollY, setLastScrollY] = useState(0);

    /* ── scroll: shadow + hide-on-scroll-down ── */
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

    /* ── lock body scroll when mobile menu is open ── */
    useEffect(() => {
        document.body.style.overflow = menuOpen ? "hidden" : "";
        return () => { document.body.style.overflow = ""; };
    }, [menuOpen]);

    return (
        <>
            <header
                className={[
                    styles.header,
                    scrolled ? styles.scrolled : "",
                    !visible ? styles.hidden : "",
                    menuOpen ? styles.menuOpen : "",
                ].join(" ")}
            >
                <div className={styles.inner}>

                    {/* ── Logo ── */}
                    <a href="/" className={styles.logo} aria-label="FinanceCore home">
                        <span className={styles.logoMark}>FC</span>
                        <span className={styles.logoText}>FinanceCore</span>
                    </a>

                    {/* ── Desktop nav links ── */}
                    <nav className={styles.desktopNav} aria-label="Main navigation">
                        {navLinks.map((link) => (
                            <a
                                key={link.page}
                                href={link.href}
                                className={[
                                    styles.navLink,
                                    activePage === link.page ? styles.navLinkActive : "",
                                ].join(" ")}
                            >
                                {link.label}
                                {activePage === link.page && (
                                    <span className={styles.activeDot} aria-hidden="true" />
                                )}
                            </a>
                        ))}
                    </nav>

                    {/* ── Desktop CTAs ── */}
                    <div className={styles.desktopActions}>
                        <a href="/login" className={styles.loginBtn}>Log In</a>
                        <a href="/register" className={styles.ctaBtn}>Get Started Free</a>
                    </div>

                    {/* ── Mobile hamburger ── */}
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

            {/* ── Mobile drawer ── */}
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
                    <span>🔒 SOC 2 Certified</span>
                    <span>·</span>
                    <span>99.9% Uptime</span>
                </div>
            </div>

            {/* ── Mobile backdrop ── */}
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