import { useEffect, useRef, useState } from "react";
import styles from "./Select.module.css";
import { ChevronDown } from "lucide-react";
import type {CustomSelectProps} from "./types";

export default function CustomSelect({
    value,
    onChange,
    options,
    placeholder = "Select...",
    variant = 'secondary'
}: CustomSelectProps) {
    const [open, setOpen] = useState(false);
    const ref = useRef<HTMLDivElement>(null);

    const selectedLabel =
        options.find((opt) => opt.value === value)?.label || placeholder;

    useEffect(() => {
        function handleClickOutside(e: MouseEvent) {
            if (ref.current && !ref.current.contains(e.target as Node)) {
                setOpen(false);
            }
        }

        document.addEventListener("mousedown", handleClickOutside);
        return () => document.removeEventListener("mousedown", handleClickOutside);
    }, []);

    // variant style 
    const btnStyle = `${styles.trigger} ${styles[variant]}`;

    return (
        <div className={styles.wrapper} ref={ref}>
            {/* Trigger */}
            <button
                type="button"
                className={btnStyle}
                onClick={() => setOpen((prev) => !prev)}
            >
                {selectedLabel}
                <span className={styles.arrow}><ChevronDown size={15} /></span>
            </button>

            {/* Dropdown */}
            {open && (
                <div className={styles.dropdown}>
                    {options.map((opt) => (
                        <div
                            key={opt.value}
                            className={`${styles.option} ${opt.value === value ? styles.active : ""
                                }`}
                            onClick={() => {
                                onChange(opt.value);
                                setOpen(false);
                            }}
                        >
                            {opt.label}
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
}