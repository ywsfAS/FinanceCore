import type { DeleteTransactionParams, ImportTransactionsParams } from "../../services/transactionService";
import TransactionImportPopup from "../TransactionImportPopup/TransactionImportPopup";
import { TransactionRemovePopUp } from "../TransactionRemovePopUp/TransactionRemovePopUp";
import { TransactionViewPopUp } from "../TransactionViewPopUp/TransactionViewPopUp";

export type TransactionActions = "export" | "import" | "id" | "remove";
export type ImportedFileType = "Csv";
export interface TransactionActionProps {
    id: string;
    action: TransactionActions;
    onClose: () => void;
}

export const TransactionAction = ({ onClose, id, action }: TransactionActionProps) => {

    return (
        <div>
            {action == "id" && (
                <TransactionViewPopUp id={id} onClose={onClose} />
            )}
            {action == "import" && (
                <TransactionImportPopup onClose={onClose} />
            )}
            {action == "remove" && (
                <TransactionRemovePopUp onClose={onClose} />
            )}


        </div>
    )
}
