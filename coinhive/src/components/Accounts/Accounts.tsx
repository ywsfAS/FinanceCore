import styles from "./Accounts.module.css";
import { useState } from "react";
import { Banknote, Search, WalletCards } from "lucide-react";
import { motion } from "motion/react";

import AccountCard from "../AccountCard/AccountCard";
import AccountCreatePopUp from "../AccountCreatePopup/AccountCreatePopup";
import AccountActionPopup, { type AccountAction } from "../AccountActionPopup/AccountActionPopup";
import Input from "../Input/Input";
import CustomSelect from "../Select/Select";
import SectionHeader from "../SectionHeader/SectionHeader";
import { useGetAccountsWithFilters } from "../../hooks/Account/useGetAccountWithFilters";
import { useGetAccountById } from "../../hooks/Account/useGetAccountById";
import { useRemoveAccount } from "../../hooks/Account/useRemoveAccount";
import { useUpdateAccount } from "../../hooks/Account/useUpdateAccount";
import { useCreateAccountAlert } from "../../hooks/Account/useCreateAccountAlert";
import { useReconcileAccount } from "../../hooks/Account/useReconcileAccount";
import type { GetAccountWithFiltersParams } from "../../services/accountService";
import type { AccountEntity } from "../../entities/Account";
import { ACCOUNT_TYPES, CURRENCIES, INITIAL_FILTERS, HEADER } from "./constants";

interface SelectedAccount {
    id: string;
    name: string;
    type: string;
}

const Accounts = () => {
    const [open, setOpen] = useState(false);
    const [accountId, setAccountId] = useState("");
    const [idSearch, setIdSearch] = useState("");
    const [selectedAccount, setSelectedAccount] = useState<SelectedAccount | null>(null);
    const [action, setAction] = useState<AccountAction | null>(null);
    const [openMenuId, setOpenMenuId] = useState<string | null>(null);
    const [draggedAccountId, setDraggedAccountId] = useState<string | null>(null);
    const [accountOrder, setAccountOrder] = useState<string[]>([]);
    const [filters, setFilters] = useState<GetAccountWithFiltersParams>(INITIAL_FILTERS);

    const { data, isLoading } = useGetAccountsWithFilters(filters);
    const accountById = useGetAccountById({ id: idSearch });
    const removeAccount = useRemoveAccount();
    const updateAccount = useUpdateAccount();
    const createAlert = useCreateAccountAlert();
    const reconcileAccount = useReconcileAccount();
    const accounts: AccountEntity[] = Array.isArray(data) ? data : [];

    const updateFilter = (key: keyof GetAccountWithFiltersParams, value: string) => {
        setFilters((previous) => ({ ...previous, [key]: value }));
    };

    const closeAction = () => {
        setAction(null);
        setSelectedAccount(null);
    };

    const openAction = (nextAction: AccountAction, account: SelectedAccount) => {
        setSelectedAccount(account);
        setAction(nextAction);
    };

    const searchById = () => {
        const id = accountId.trim();
        setIdSearch(id);
    };

    const handleDelete = async () => {
        if (!selectedAccount) return;
        await removeAccount.mutateAsync({ id: selectedAccount.id });
        closeAction();
    };

    const reorderAccounts = (targetId: string) => {
        if (!draggedAccountId || draggedAccountId === targetId) return;
        setAccountOrder((previous) => {
            const next = previous.length > 0 ? [...previous] : accounts.map((account) => account.id);
            const draggedIndex = next.indexOf(draggedAccountId);
            const targetIndex = next.indexOf(targetId);
            if (draggedIndex === -1 || targetIndex === -1) return previous;
            next.splice(draggedIndex, 1);
            const adjustedTargetIndex = draggedIndex < targetIndex ? targetIndex - 1 : targetIndex;
            next.splice(adjustedTargetIndex, 0, draggedAccountId);
            return next;
        });
        setDraggedAccountId(null);
    };

    if (isLoading) return <div>Loading...</div>;

    const visibleAccounts: AccountEntity[] = idSearch
        ? accountById.data
            ? [{ ...accountById.data, type: accountById.data.type as AccountEntity["type"] }]
            : []
        : accounts;
    const orderedVisibleAccounts = [...visibleAccounts].sort(
        (first, second) => {
            const firstPosition = accountOrder.indexOf(first.id);
            const secondPosition = accountOrder.indexOf(second.id);
            return (firstPosition === -1 ? Number.MAX_SAFE_INTEGER : firstPosition)
                - (secondPosition === -1 ? Number.MAX_SAFE_INTEGER : secondPosition);
        }
    );
    const totalBalance = accounts.reduce((total, account) => total + account.balance, 0);
    const currencies = new Set(accounts.map((account) => account.currency));
    const balanceLabel = currencies.size === 1 ? Array.from(currencies)[0] : "Mixed";

    return (
        <div className={styles.wrapper} onClick={() => setOpenMenuId(null)}>
            <SectionHeader title={HEADER.title} subtitle={HEADER.subtitle} btnName={HEADER.btnName} handler={() => setOpen(true)} />
            <motion.div
                className={styles.summaryGrid}
                initial="hidden"
                animate="visible"
                variants={{ visible: { transition: { staggerChildren: 0.08 } } }}
            >
                <motion.div className={styles.summaryCard} variants={{ hidden: { opacity: 0, y: 10 }, visible: { opacity: 1, y: 0 } }}>
                    <WalletCards className={styles.summaryIcon} size={20} />
                    <div><span>Tracked accounts</span><strong>{accounts.length}</strong></div>
                </motion.div>
                <motion.div className={styles.summaryCard} variants={{ hidden: { opacity: 0, y: 10 }, visible: { opacity: 1, y: 0 } }}>
                    <Banknote className={styles.summaryIcon} size={20} />
                    <div><span>Combined balance</span><strong>{totalBalance.toLocaleString(undefined, { maximumFractionDigits: 2 })} <small>{balanceLabel}</small></strong></div>
                </motion.div>
            </motion.div>
            <div className={styles.filterSection}>
                <div className={styles.filterHeading}>
                    <span className={styles.filterEyebrow}>Account directory</span>
                    <span className={styles.filterHint}>Find and narrow your accounts</span>
                </div>
                <div className={styles.searchRow}>
                    <div className={styles.searchContainer}>
                        <Input placeholder="Search account..." value={filters.name} onChange={(event) => updateFilter("name", event.target.value)} />
                    </div>
                    <div className={styles.idSearchContainer}>
                        <Input placeholder="Search by account ID..." value={accountId} onChange={(event) => setAccountId(event.target.value)} />
                        <button type="button" className={styles.iconButton} title="Search by account ID" onClick={searchById}><Search size={16} /></button>
                    </div>
                </div>
                <div className={styles.filterOptions}>
                    <CustomSelect value={filters.type} onChange={(value) => updateFilter("type", value)} options={ACCOUNT_TYPES} variant="secondary" />
                    <CustomSelect value={filters.currency} onChange={(value) => updateFilter("currency", value)} options={CURRENCIES} variant="secondary" />
                </div>
            </div>

            <div className={styles.accountsGrid}>
                {orderedVisibleAccounts.length > 0 ? orderedVisibleAccounts.map((account) => (
                    <AccountCard
                        key={account.id}
                        id={account.id}
                        name={account.name}
                        type={account.type}
                        balance={account.balance}
                        currency={account.currency}
                        onView={(id) => openAction("id", { id, name: account.name, type: String(account.type) })}
                        onEdit={(id, name, type) => openAction("edit", { id, name, type })}
                        onDelete={(id) => openAction("delete", { id, name: account.name, type: String(account.type) })}
                        onAlert={(id) => openAction("alert", { id, name: account.name, type: String(account.type) })}
                        onReconcile={(id) => openAction("reconcile", { id, name: account.name, type: String(account.type) })}
                        menuOpen={openMenuId === account.id}
                        onMenuOpen={setOpenMenuId}
                        onMenuClose={() => setOpenMenuId(null)}
                        onDragStart={setDraggedAccountId}
                        onDrop={reorderAccounts}
                        onDragEnd={() => setDraggedAccountId(null)}
                    />
                )) : <div>No accounts available.</div>}
            </div>

            {open && <AccountCreatePopUp handleClose={() => setOpen(false)} />}
            {action && selectedAccount && (
                <AccountActionPopup
                    action={action}
                    {...selectedAccount}
                    onClose={closeAction}
                    onEdit={async (name, type) => { await updateAccount.mutateAsync({ id: selectedAccount.id, name, type }); closeAction(); }}
                    onAlert={async (thresholdAmount) => { await createAlert.mutateAsync({ accountId: selectedAccount.id, thresholdAmount }); closeAction(); }}
                    onReconcile={async (actualBalance) => { await reconcileAccount.mutateAsync({ accountId: selectedAccount.id, actualBalance, reason: "CountingCorrection", createAdjustment: true }); closeAction(); }}
                    onDelete={handleDelete}
                />
            )}
        </div>
    );
};

export default Accounts;
