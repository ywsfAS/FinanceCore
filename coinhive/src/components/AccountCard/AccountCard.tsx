import styles from './AccountCard.module.css';
import { motion } from "motion/react";
import AccountContextMenu from "../AccountContextMenu/AccountContextMenu";
import { ACCOUNT_TYPE_ICONS } from "../Accounts/constants";
import type { AccountCardProps } from "./types"

const AccountCard = ({
    id,
    name,
    type,
    balance,
    currency,
    onView,
    onEdit,
    onDelete,
    onAlert,
    onReconcile,
    menuOpen,
    onMenuOpen,
    onMenuClose,
    onDragStart,
    onDrop,
    onDragEnd,
}: AccountCardProps) => {
    const AccountIcon = ACCOUNT_TYPE_ICONS[type as keyof typeof ACCOUNT_TYPE_ICONS] ?? ACCOUNT_TYPE_ICONS.Other;
    const openMenu = (event: React.MouseEvent<HTMLDivElement>) => {
        event.preventDefault();
        onMenuOpen(id);
    };

    return (
        <div
            className={styles.dropZone}
            onDragOver={(event) => event.preventDefault()}
            onDrop={() => onDrop(id)}
        >
            <motion.article
                className={styles.card}
                id={id}
                draggable
                onContextMenu={openMenu}
                onDragStart={() => onDragStart(id)}
                onDragEnd={onDragEnd}
                initial={{ opacity: 0, y: 14 }}
                animate={{ opacity: 1, y: 0 }}
                whileHover={{ scale: 1.015 }}
                transition={{ duration: 0.28, ease: "easeOut" }}
            >
                <div className={styles.top}>
                    <div className={styles.iconShell}><AccountIcon size={20} /></div>
                    <div className={styles.heading}>
                        <span className={styles.kicker}>Account</span>
                        <h3>{name}</h3>
                    </div>
                </div>
                <div className={styles.metaRow}>
                    <span className={styles.type}>{type}</span>
                    <span className={styles.status}><span /> Active</span>
                </div>
                <div className={styles.balanceBlock}>
                    <span className={styles.balanceLabel}>Available balance</span>
                    <div className={styles.balance}>
                        {balance.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}
                        <span className={styles.currency}>{currency}</span>
                    </div>
                </div>
                {menuOpen && (
                    <AccountContextMenu
                        id={id}
                        type={String(type)}
                        onClose={onMenuClose}
                        onView={onView}
                        onEdit={() => onEdit?.(id, name, type)}
                        onAlert={onAlert}
                        onReconcile={onReconcile}
                        onDelete={onDelete}
                    />
                )}
            </motion.article>
        </div>
    );
};

export default AccountCard;