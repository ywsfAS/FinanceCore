import React, { useEffect, useState } from "react";
import styles from "./Navbar.module.css";
import { useTheme } from "../../hooks/Theme/Theme";
import { Link, NavLink } from "react-router-dom";
import Button from "../Button/Button";
import { Moon, Sun } from "lucide-react";

const navLinks = [
    { label: "Product", href: "/", end: true },
    { label: "Profile", href: "/profile" },
    { label: "Pricing", href: "/pricing" },
    { label: "About", href: "/about" },
    { label: "Contact", href: "/contact" },
];

const Navbar: React.FC = () => {
    const [scrolled, setScrolled] = useState(false);
    const [menuOpen, setMenuOpen] = useState(false);
    const [visible, setVisible] = useState(true);
    const [lastScrollY, setLastScrollY] = useState(0);

    const { theme, toggleTheme } = useTheme();

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

        return () => {
            document.body.style.overflow = "";
        };
    }, [menuOpen]);

    return (
        <>
            <header
                className={[
                    styles.header,
                    theme === "dark" ? styles.dark : "",
                    scrolled ? styles.scrolled : "",
                    !visible ? styles.hidden : "",
                    menuOpen ? styles.menuOpen : "",
                ].join(" ")}
            >
                <div className={styles.inner}>

                    {/* Logo */}
                    <Link
                        to="/"
                        className={styles.logo}
                        aria-label="FinanceCore home"
                    >
                        <span className={styles.logoMark}>FC</span>
                        <span className={styles.logoText}>
                            FinanceCore
                        </span>
                    </Link>

                    {/* Desktop Navigation */}
                    <nav
                        className={styles.desktopNav}
                        aria-label="Main navigation"
                    >
                        {navLinks.map((link) => (
                            <NavLink
                                key={link.href}
                                to={link.href}
                                end={link.end}
                                className={({ isActive }) =>
                                    [
                                        styles.navLink,
                                        isActive
                                            ? styles.navLinkActive
                                            : "",
                                    ].join(" ")
                                }
                            >
                                {({ isActive }) => (
                                    <>
                                        {link.label}

                                        {isActive && (
                                            <span
                                                className={styles.activeDot}
                                                aria-hidden="true"
                                            />
                                        )}
                                    </>
                                )}
                            </NavLink>
                        ))}
                    </nav>

                    {/* Desktop Actions */}
                    <div className={styles.desktopActions}>
                        <Link
                            to="/login"
                            className={styles.loginBtn}
                        >
                            Login
                        </Link>

                        <Link
                            to="/register"
                            className={styles.ctaBtn}
                        >
                            Get Started Free
                        </Link>

                        <Button
                            onClick={toggleTheme}
                            variant="primary"
                            size="small"
                        >
                            {theme === "dark" ? (
                                <Moon size={20} />
                            ) : (
                                <Sun size={20} />
                            )}
                        </Button>
                    </div>

                    {/* Hamburger */}
                    <button
                        className={styles.hamburger}
                        onClick={() => setMenuOpen((o) => !o)}
                        aria-label={
                            menuOpen
                                ? "Close menu"
                                : "Open menu"
                        }
                        aria-expanded={menuOpen}
                    >
                        <span className={styles.bar} />
                        <span className={styles.bar} />
                        <span className={styles.bar} />
                    </button>
                </div>
            </header>

            {/* Mobile Drawer */}
            <div
                className={[
                    styles.mobileDrawer,
                    menuOpen ? styles.drawerOpen : "",
                ].join(" ")}
                aria-hidden={!menuOpen}
            >
                <nav
                    className={styles.mobileNav}
                    aria-label="Mobile navigation"
                >
                    {navLinks.map((link) => (
                        <NavLink
                            key={link.href}
                            to={link.href}
                            end={link.end}
                            className={({ isActive }) =>
                                [
                                    styles.mobileLink,
                                    isActive
                                        ? styles.mobileLinkActive
                                        : "",
                                ].join(" ")
                            }
                            onClick={() => setMenuOpen(false)}
                        >
                            {link.label}
                        </NavLink>
                    ))}
                </nav>

                {/* Mobile Actions */}
                <div className={styles.mobileActions}>
                    <Link
                        to="/login"
                        className={styles.mobileLogin}
                        onClick={() => setMenuOpen(false)}
                    >
                        Log In
                    </Link>

                    <Link
                        to="/register"
                        className={styles.mobileCta}
                        onClick={() => setMenuOpen(false)}
                    >
                        Get Started Free
                    </Link>
                </div>

                {/* Mobile Footer */}
                <div className={styles.mobileFooter}>
                    <span>SOC 2 Certified</span>
                    <span>·</span>
                    <span>99.9% Uptime</span>
                </div>
            </div>

            {/* Backdrop */}
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