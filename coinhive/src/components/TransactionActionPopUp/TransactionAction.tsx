import { X } from "lucide-react";
import { DeleteConfirmationPopup } from "../DeleteConfirmationPopup/DeleteConfirmationPopup";
import TransactionImportPopup from "../TransactionImportPopup/TransactionImportPopup";
import { ViewDetailsPopup } from "../ViewDetailsPopup/ViewDetailsPopup";
import styles from "./TransactionAction.module.css";

export type TransactionActions =
    | "export"
    | "import"
    | "id"
    | "remove";

export type ImportedFileType = "Csv";

export interface TransactionActionProps {
    id: string;
    name: string;
    action: TransactionActions;
    onClose: () => void;
    onDelete: () => void;
}

const TITLES: Record<TransactionActions, string> = {
    export: "Export transactions",
    import: "Import transactions",
    id: "Transaction ID",
    remove: "Delete transaction",
};

const DESCRIPTIONS: Record<TransactionActions, string> = {
    export: "Export your transaction data.",
    import: "Import transactions from a CSV file.",
    id: "View the unique identifier used to find this transaction.",
    remove: "Remove this transaction permanently. This action cannot be undone.",
};

export const TransactionAction = ({
    onClose,
    id,
    action,
    name,
    onDelete,
}: TransactionActionProps) => {
    return (
        <div
            className={styles.overlay}
            onMouseDown={(event) =>
                event.target === event.currentTarget && onClose()
            }
        >
            <div
                className={styles.popup}
                role="dialog"
                aria-modal="true"
                aria-labelledby="transaction-action-title"
            >
                <div className={styles.header}>
                    <div>
                        <h2 id="transaction-action-title">
                            {TITLES[action]}
                        </h2>

                        <p className={styles.description}>
                            {DESCRIPTIONS[action]}
                        </p>
                    </div>

                    <button
                        className={styles.closeButton}
                        type="button"
                        onClick={onClose}
                        aria-label="Close popup"
                    >
                        <X />
                    </button>
                </div>

                {action === "id" && (
                    <ViewDetailsPopup
                        id={id}
                        onClose={onClose}
                    />
                )}

                {action === "import" && (
                    <TransactionImportPopup
                        onClose={onClose}
                    />
                )}

                {action === "remove" && (
                    <DeleteConfirmationPopup
                        name={name}
                        onClose={onClose}
                        onDelete={onDelete}
                    />
                )}

                {action === "export" && (
                    <div>

                    </div>
                )}
            </div>
        </div>
    );
};

export default TransactionAction;