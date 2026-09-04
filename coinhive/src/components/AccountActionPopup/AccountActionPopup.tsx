import { useState, type FormEvent } from "react";
import { X } from "lucide-react";
import Button from "../Button/Button";
import Input from "../Input/Input";
import CustomSelect from "../Select/Select";
import { ACCOUNT_TYPES } from "../Accounts/constants";
import styles from "./AccountActionPopup.module.css";
import { ViewDetailsPopup } from "../ViewDetailsPopup/ViewDetailsPopup";
import { VIEW_ACCOUNT_TITLE } from "./constants";
import { DeleteConfirmationPopup } from "../DeleteConfirmationPopup/DeleteConfirmationPopup";

export type AccountAction = "id" | "edit" | "alert" | "reconcile" | "delete";

interface AccountActionPopupProps {
    action: AccountAction;
    id: string;
    name: string;
    type: string;
    onClose: () => void;
    onEdit: (name: string, type: string) => void;
    onAlert: (thresholdAmount: number) => void;
    onReconcile: (actualBalance: number) => void;
    onDelete: () => void;
}

const TITLES: Record<AccountAction, string> = {
    id: "Account ID",
    edit: "Update account",
    alert: "Balance alert",
    reconcile: "Reconcile cash account",
    delete: "Delete account",
};

const DESCRIPTIONS: Record<AccountAction, string> = {
    id: "View the unique identifier used to find this account and connect it with your records.",
    edit: "Update your account details, including its name and account type.",
    alert: "Create a notification that helps you track when this account reaches your chosen balance threshold.",
    reconcile: "Compare the recorded balance with your actual cash balance and create an adjustment when needed.",
    delete: "Remove this account and its details from your account list. This action cannot be undone.",
};

const AccountActionPopup = ({ action, id, name, type, onClose, onEdit, onAlert, onReconcile, onDelete }: AccountActionPopupProps) => {
    const [accountName, setAccountName] = useState(name);
    const [accountType, setAccountType] = useState(type);
    const [amount, setAmount] = useState("");

    const submit = (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const numericAmount = Number(amount);
        if (action === "edit" && accountName.trim() && accountType) onEdit(accountName.trim(), accountType);
        if (action === "alert" && Number.isFinite(numericAmount)) onAlert(numericAmount);
        if (action === "reconcile" && Number.isFinite(numericAmount)) onReconcile(numericAmount);
    };

    return (
        <div className={styles.overlay} onMouseDown={(event) => event.target === event.currentTarget && onClose()}>
            <div className={styles.popup} role="dialog" aria-modal="true" aria-labelledby="account-action-title">
                <div className={styles.header}>
                    <div>
                        <h2 id="account-action-title">{TITLES[action]}</h2>
                        <p className={styles.description}>{DESCRIPTIONS[action]}</p>
                    </div>
                    <button className={styles.closeButton} type="button" onClick={onClose} aria-label="Close popup"><X /></button>
                </div>

                {action === "id" && (
                    <ViewDetailsPopup id={id} title={VIEW_ACCOUNT_TITLE} onClose={onClose} />
                )}

                {action === "delete" && (
                    <DeleteConfirmationPopup name={name} onClose={onClose} onDelete={onDelete} />
                )}

                {action !== "id" && action !== "delete" && (
                    <form className={styles.form} onSubmit={submit}>
                        {action === "edit" && <>
                            <Input label="Name" value={accountName} onChange={(event) => setAccountName(event.target.value)} required />
                            <div className={styles.field}><label>Type</label><CustomSelect value={accountType} onChange={setAccountType} options={ACCOUNT_TYPES.filter((option) => option.value)} variant="secondary" /></div>
                        </>}
                        {action === "alert" && <Input label="Alert threshold" type="number" min="0" step="0.01" value={amount} onChange={(event) => setAmount(event.target.value)} required />}
                        {action === "reconcile" && <Input label="Actual cash balance" type="number" step="0.01" value={amount} onChange={(event) => setAmount(event.target.value)} required />}
                        <Button type="submit">{action === "alert" ? "Set alert" : action === "reconcile" ? "Reconcile account" : "Save changes"}</Button>
                    </form>
                )}
            </div>
        </div>
    );
};

export default AccountActionPopup;