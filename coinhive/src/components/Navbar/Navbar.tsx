import React, { useState } from "react";
import styles from "./Navbar.module.css";
import { useTheme } from "../../hooks/Theme/Theme";
import { Link, NavLink } from "react-router-dom";
import Button from "../Button/Button";
import { Moon, Sun } from "lucide-react";
import { motion } from 'motion/react';
import MobileMenu from "./MobileMenu";
import Hamburger from "./Hamburger";

const navLinks = [
    { label: "Home", href: "/", end: true },
    { label: "Dashboard", href: "/profile" },
    { label: "Help", href: "/about" },
];

const Navbar: React.FC = () => {
    const [menuOpen, setMenuOpen] = useState(false);

    const { theme, toggleTheme } = useTheme();

    return (
        <>
            <motion.header className={styles.header}
                initial={{ y: -100, opacity: 0 }}
                animate={{ y: 0, opacity: 1 }}
                transition={{
                    type: "spring",
                    stiffness: 120,
                    damping: 18,
                }}
            >
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
                                {({ isActive }) => (
                                    <>
                                        {isActive && (
                                            <motion.div
                                                layoutId="activeNavLink"
                                                className={styles.activeBackground}
                                                transition={{
                                                    type: "spring",
                                                    stiffness: 480,
                                                    damping: 20,
                                                }}
                                            />
                                        )}
                                        <span>{link.label}</span>
                                    </>
                                )}
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

                        <button
                            className={styles.toggleBackground}
                            onClick={toggleTheme}
                        >
                            {theme === "dark" ? (
                                <motion.div
                                    key="moon"
                                    initial={{ opacity: 0, rotate: -90, scale: 0 }}
                                    animate={{ opacity: 1, rotate: 0, scale: 1 }}
                                    exit={{ opacity: 0, rotate: 90, scale: 0 }}
                                    transition={{ duration: 0.25 }}
                                >
                                    <Moon size={18} />
                                </motion.div>
                            ) : (
                                <motion.div
                                    key="sun"
                                    initial={{ opacity: 0, rotate: 90, scale: 0 }}
                                    animate={{ opacity: 1, rotate: 0, scale: 1 }}
                                    exit={{ opacity: 0, rotate: -90, scale: 0 }}
                                    transition={{ duration: 0.25 }}
                                >
                                    <Sun size={18} />
                                </motion.div>
                            )}
                        </button>
                    </div>

                    <Hamburger
                        open={menuOpen}
                        onClick={() => setMenuOpen(!menuOpen)}
                    />
                </div>
            </motion.header>
            <MobileMenu
                open={menuOpen}
                onClose={() => setMenuOpen(false)}
                links={navLinks}
            />

        </>
    );
};

export default Navbar;