import Button from "../Button/Button";
import styles from "./DeleteConfirmationPopup.module.css";

export interface DeleteConfirmationPopupProps {
    name: string;
    onClose: () => void;
    onDelete: () => void;

}
export const DeleteConfirmationPopup = ({ name, onClose ,onDelete}: DeleteConfirmationPopupProps) => {

    return (
        <div className={styles.idContent}>
            <p>Delete <strong>{name}</strong>? This action cannot be undone.</p>
            <div className={styles.buttonRow}><Button type="button" variant="secondary" onClick={onClose}>Cancel</Button><Button type="button" variant="danger" onClick={onDelete}>Delete account</Button></div>
        </div>
    )
}
