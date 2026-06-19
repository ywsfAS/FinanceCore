import styles from "./MobileMenu.module.css";
import { NavLink } from "react-router-dom";
import Button from "../Button/Button";
export interface MobileInfoProps {
    open: boolean;
    onClose: () => void;
    links: Record<string,string | boolean | undefined>[];
}
export default function MobileMenu({ open, onClose, links } : MobileInfoProps) {
    return (
        <>
            {/* Drawer */}
            <div
                className={`${styles.mobileDrawer} ${open ? styles.drawerOpen : ""
                    }`}
                aria-hidden={!open}
            >
                <nav className={styles.mobileNav}>
                    {links.map((link) => (
                        <NavLink
                            key={link.href}
                            to={link.href}
                            className={({ isActive }) =>
                                `${styles.mobileLink} ${isActive ? styles.mobileLinkActive : ""
                                }`
                            }
                            onClick={onClose}
                        >
                            {link.label}
                        </NavLink>
                    ))}
                </nav>

                <div className={styles.mobileActions}>
                    <Button onClick={onClose} >
                        Log in
                    </Button>

                    <Button  onClick={onClose}>
                        Get Started
                    </Button>
                </div>
            </div>

            {/* Backdrop */}
            {open && (
                <div className={styles.backdrop} onClick={onClose} />
            )}
        </>
    );
}