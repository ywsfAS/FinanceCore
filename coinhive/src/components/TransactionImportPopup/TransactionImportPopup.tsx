import { useState } from "react";
import { X } from "lucide-react";
import Button from "../Button/Button";
import CustomSelect from "../Select/Select";
import { useUserAccountsOptions } from "../../hooks/User/useUserAccountsOptions";
import styles from "./TransactionImportPopup.module.css";
import { useImportTransaction } from "../../hooks/Transactions/useImportTransaction";

interface TransactionImportPopupProps {
    onClose: () => void;

}

const TransactionImportPopup = ({ onClose }: TransactionImportPopupProps) => {
    const [accountId, setAccountId] = useState("");
    const [file, setFile] = useState<File | null>(null);
    const { data: accounts = [] } = useUserAccountsOptions();
    const importMutation = useImportTransaction();

    const submit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        if (!file || !accountId) return;
        await importMutation.mutateAsync({ type: "Csv", file, accountId });
        onClose();
    };
    return <div className={styles.overlay}><form className={styles.popup} onSubmit={submit}>
        <div className={styles.header}><div><h2>Import transactions</h2><p>Upload a CSV file and assign its transactions to an account.</p></div><button type="button" onClick={onClose} aria-label="Close"><X /></button></div>
        <label>Account<CustomSelect value={accountId} options={[{ value: "", label: "Select account" }, ...accounts.map((account) => ({ value: account.id, label: account.name }))]} onChange={setAccountId} variant="secondary" /></label>
        <label>CSV file<input type="file" accept=".csv,text/csv" onChange={(event) => setFile(event.target.files?.[0] ?? null)} required /></label>
        {importMutation.isError && <p className={styles.error}>Could not import this file. Check the format and try again.</p>}
        <div className={styles.actions}><Button type="button" variant="secondary" onClick={onClose}>Cancel</Button><Button type="submit" disabled={!file || !accountId || importMutation.isPending}>{importMutation.isPending ? "Importing..." : "Import CSV"}</Button></div>
    </form></div>;
};

export default TransactionImportPopup;
