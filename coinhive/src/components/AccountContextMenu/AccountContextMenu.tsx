import { Bell, MessageCircleDashed, Pencil, Search, Trash2 } from "lucide-react";
import styles from "./AccountContextMenu.module.css";

interface AccountContextMenuProps {
    id: string;
    type: string;
    onClose: () => void;
    onView?: (id: string) => void;
    onEdit?: (id: string) => void;
    onDelete?: (id: string) => void;
    onAlert?: (id: string) => void;
    onReconcile?: (id: string) => void;
}

const AccountContextMenu = ({ id, type, onClose, onView, onEdit, onDelete, onAlert, onReconcile }: AccountContextMenuProps) => {
    const runAction = (callback: ((accountId: string) => void) | undefined) => {
        callback?.(id);
        onClose();
    };

    return (
        <div className={styles.menu} role="menu" onClick={(event) => event.stopPropagation()}>
            {onView && <button type="button" role="menuitem" onClick={() => runAction(onView)}><Search size={19} /><span><strong>View ID</strong><small>See and copy the account identifier</small></span></button>}
            {onEdit && <button type="button" role="menuitem" onClick={() => runAction(onEdit)}><Pencil size={19} /><span><strong>Edit account</strong><small>Change your account details</small></span></button>}
            {onAlert && <button type="button" role="menuitem" onClick={() => runAction(onAlert)}><Bell size={19} /><span><strong>Set alert</strong><small>Get notified below a balance</small></span></button>}
            {onReconcile && type.toLowerCase() === "cash" && <button type="button" role="menuitem" onClick={() => runAction(onReconcile)}><MessageCircleDashed size={19} /><span><strong>Reconcile</strong><small>Match your cash with its actual balance</small></span></button>}
            {onDelete && <button type="button" role="menuitem" className={styles.deleteButton} onClick={() => runAction(onDelete)}><Trash2 size={19} /><span><strong>Delete account</strong><small>Remove this account permanently</small></span></button>}
        </div>
    );
};

export default AccountContextMenu;