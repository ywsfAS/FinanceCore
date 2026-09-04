import Button from "../Button/Button"
import styles from "./ViewDetailsPopup.module.css";
import { Check, Copy } from "lucide-react";
import { useState } from "react";

export interface ViewDetailsPopup {
    title?: string;
    id: string;
    onClose?: () => void;
}
export const ViewDetailsPopup = ({ title, id, onClose }: ViewDetailsPopup) => {
    const [copied, setCopied] = useState(false);

    const copyId = async () => {
        await navigator.clipboard.writeText(id);
        setCopied(true);
    };
    return (
        <div className={styles.idContent}>
            <p>{title}</p>
            <div className={styles.idValue}>{id}</div>
            <div className={styles.btns}>
                <Button type="button" onClick={onClose} variant="secondary">Close</Button>
                <Button type="button" onClick={copyId}>{copied ? <><Check size={18} /> Copied</> : <><Copy size={18} /> Copy ID</>}</Button>
            </div>
        </div>
    )
}
