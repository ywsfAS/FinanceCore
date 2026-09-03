import { useState } from "react";
import { X } from "lucide-react";
import Button from "../Button/Button";
import CustomSelect from "../Select/Select";
import { useUserAccountsOptions } from "../../hooks/User/useUserAccountsOptions";
import { useUserCategoriesOptions } from "../../hooks/User/useUserCategoriesOptions";
import { useExportTransaction } from "../../hooks/Transactions/useExportTransaction";
import { TransactionType, type ExportTransactionsParams } from "../../services/transactionService";
import styles from "./TransactionExportPopup.module.css";

interface TransactionExportPopupProps {
    onClose: () => void;
    onExport: () => void;

}

const TransactionExportPopup = ({ onClose}: TransactionExportPopupProps) => {
    const [filters, setFilters] = useState<ExportTransactionsParams>({ Page: 1, PageSize: 100 });
    const { data: accounts = [] } = useUserAccountsOptions();
    const { data: categories = [] } = useUserCategoriesOptions();
    const exportMutation = useExportTransaction();

    const update = (key: keyof ExportTransactionsParams, value: string) => {
        setFilters((previous) => ({
            ...previous,
            [key]: key === "Start" || key === "End"
                ? value ? new Date(value) : undefined
                : value || undefined,
        }));
    };

    const submit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const file = await exportMutation.mutateAsync(filters);
        const url = URL.createObjectURL(file);
        const link = document.createElement("a");
        link.href = url;
        link.download = "transactions-export.csv";
        link.click();
        URL.revokeObjectURL(url);
        onClose();
    };

    return <div className={styles.overlay}><form className={styles.popup} onSubmit={submit}>
        <div className={styles.header}><div><h2>Export transactions</h2><p>Choose the transaction data you want to download.</p></div><button type="button" onClick={onClose} aria-label="Close"><X /></button></div>
        <div className={styles.grid}>
            <label>Account<CustomSelect value={filters.accountId} options={[{ value: "", label: "All accounts" }, ...accounts.map((account) => ({ value: account.id, label: account.name }))]} onChange={(value) => update("accountId", value)} variant="secondary" /></label>
            <label>To account<CustomSelect value={filters.toAccountId} options={[{ value: "", label: "All destination accounts" }, ...accounts.map((account) => ({ value: account.id, label: account.name }))]} onChange={(value) => update("toAccountId", value)} variant="secondary" /></label>
            <label>Category<CustomSelect value={filters.CategoryId} options={[{ value: "", label: "All categories" }, ...categories.map((category) => ({ value: category.id, label: category.name }))]} onChange={(value) => update("CategoryId", value)} variant="secondary" /></label>
            <label>Type<CustomSelect value={filters.Type} options={[{ value: "", label: "All types" }, ...Object.values(TransactionType).map((type) => ({ value: type, label: type }))]} onChange={(value) => update("Type", value)} variant="secondary" /></label>
            <label>From<input type="datetime-local" value={filters.Start ? new Date(filters.Start).toISOString().slice(0, 16) : ""} onChange={(event) => update("Start", event.target.value ? new Date(event.target.value).toISOString() : "")} /></label>
            <label>To<input type="datetime-local" value={filters.End ? new Date(filters.End).toISOString().slice(0, 16) : ""} onChange={(event) => update("End", event.target.value ? new Date(event.target.value).toISOString() : "")} /></label>
        </div>
        {exportMutation.isError && <p className={styles.error}>Could not export transactions. Please try again.</p>}
        <div className={styles.actions}><Button type="button" variant="secondary" onClick={onClose}>Cancel</Button><Button type="submit" disabled={exportMutation.isPending}>{exportMutation.isPending ? "Exporting..." : "Export CSV"}</Button></div>
    </form></div>;
};

export default TransactionExportPopup;
