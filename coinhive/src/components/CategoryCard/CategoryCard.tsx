import { useState } from 'react';
import styles from './CategoryCard.module.css';
import type { CategoryCardProps, CategoryCardActionKey } from './types';
import { CATEGORY_MENU_ACTIONS, clampPercentage } from './constants';

const CategoryCard = ({
    id,
    name,
    icon,
    amount = 200,
    currency,
    percentage = 20,
    onEdit,
    onDelete,
}: CategoryCardProps) => {
    const [isMenuOpen, setIsMenuOpen] = useState(false);
    const safePercentage = clampPercentage(percentage);

    const handleAction = (key: CategoryCardActionKey) => {
        setIsMenuOpen(false);
        if (key === 'edit') onEdit?.(id);
        if (key === 'delete') onDelete?.(id);
    };

    return (
        <div className={styles.card}>
            <div className={styles.header}>
                <div className={styles.headerLeft}>
                    <span className={styles.iconWrapper}>{icon}</span>
                    <h3 className={styles.title}>{name}</h3>
                </div>

                <div className={styles.menuWrapper}>
                    <button
                        type="button"
                        className={styles.menuButton}
                        aria-haspopup="menu"
                        aria-expanded={isMenuOpen}
                        aria-label={`Open menu for ${name}`}
                        onClick={() => setIsMenuOpen((prev) => !prev)}
                    >
                        ⋮
                    </button>

                    {isMenuOpen && (
                        <div className={styles.dropdown} role="menu">
                            {CATEGORY_MENU_ACTIONS.map((action) => (
                                <button
                                    key={action.key}
                                    type="button"
                                    role="menuitem"
                                    className={
                                        action.key === 'delete'
                                            ? `${styles.dropdownItem} ${styles.dropdownItemDanger}`
                                            : styles.dropdownItem
                                    }
                                    onClick={() => handleAction(action.key)}
                                >
                                    {action.label}
                                </button>
                            ))}
                        </div>
                    )}
                </div>
            </div>

            <p className={styles.amount}>{amount}{currency}</p>
            <p className={styles.subtitle}>{safePercentage}% of total spending</p>

            <div className={styles.progressTrack}>
                <div
                    className={styles.progressFill}
                    style={{ width: `${safePercentage}%` }}
                />
            </div>
        </div>
    );
};

export default CategoryCard;