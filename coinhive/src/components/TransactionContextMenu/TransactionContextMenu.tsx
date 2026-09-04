import { Download, FileUp, Trash2 } from "lucide-react";
import styles from "./TransactionContextMenu.module.css";
import type { Coordinate } from "../TransactionCard/TransactionRow";

interface TransactionContextMenuProps {
    onClose: () => void;
    onDelete: () => void;
    onImport: () => void;
    onView: () => void;
    menuCoord: Coordinate;
}

const TransactionContextMenu = ({ onClose, onDelete, onImport, onView, menuCoord }: TransactionContextMenuProps) => {
    const run = (callback: () => void) => {
        callback();
        onClose();
    };
    const { x, y } = menuCoord;
    const posStyle = {
        left: `${x}px`,
        top: `${y}px`
    }
    return (
        <div className={styles.menu} role="menu" onClick={(event) => event.stopPropagation()} style={posStyle}>
            <button type="button" role="menuitem" onClick={() => run(onView)}><Download size={18} /><span><strong>View ID</strong><small>Download matching transaction data</small></span></button>
            <button type="button" role="menuitem" onClick={() => run(onImport)}><FileUp size={18} /><span><strong>Import CSV</strong><small>Add transactions from a CSV file</small></span></button>
            <button type="button" role="menuitem" className={styles.deleteButton} onClick={() => run(onDelete)}><Trash2 size={18} /><span><strong>Delete transaction</strong><small>Remove this transaction permanently</small></span></button>
        </div>
    );
};

export default TransactionContextMenu;
