import React, { useState } from "react";
import styles from "./Navbar.module.css";
import { useTheme } from "../../hooks/Theme/Theme";
import { Link, NavLink } from "react-router-dom";
import Button from "../Button/Button";
import { Moon, Sun } from "lucide-react";

import MobileMenu from "./MobileMenu";
import Hamburger from "./Hamburger";

const navLinks = [
    { label: "About Us", href: "/", end: true },
    { label: "Pricing", href: "/profile" },
    { label: "Services", href: "/pricing" },
    { label: "Help", href: "/about" },
];

const Navbar: React.FC = () => {
    const [menuOpen, setMenuOpen] = useState(false);

    const { theme, toggleTheme } = useTheme();

    return (
        <>
            <header className={styles.header}>
                <div className={styles.inner}>

                    {/* Logo */}
                    <Link to="/" className={styles.logo}>
                        FinanceCore
                    </Link>

                    {/* Desktop Navigation */}
                    <nav className={styles.desktopNav}>
                        {navLinks.map((link) => (
                            <NavLink
                                key={link.href}
                                to={link.href}
                                end={link.end}
                                className={({ isActive }) =>
                                    [
                                        styles.navLink,
                                        isActive ? styles.navLinkActive : "",
                                    ].join(" ")
                                }
                            >
                                {link.label}
                            </NavLink>
                        ))}
                    </nav>

                    {/* Desktop Actions */}
                    <div className={styles.desktopActions}>
                        <Link to="/login">
                            <Button size="small">Log in</Button>
                        </Link>

                        <Link to="/register">
                            <Button size="small" variant="secondary">
                                Get Started
                            </Button>
                        </Link>

                        <Button
                            onClick={toggleTheme}
                            variant="ghost"
                            size="small"
                        >
                            {theme === "dark" ? (
                                <Moon size={20} />
                            ) : (
                                <Sun size={20} />
                            )}
                        </Button>
                    </div>

                    <Hamburger
                        open={menuOpen}
                        onClick={() => setMenuOpen(!menuOpen)}
                    />
                </div>
            </header>

            <MobileMenu
                open={menuOpen}
                onClose={() => setMenuOpen(false)}
                links={navLinks}
            />
        </>
    );
};

export default Navbar;