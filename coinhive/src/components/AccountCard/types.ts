import type { EnAccountType } from "../../entities/Account";

export interface AccountCardProps {
    id: string;
    name: string;
    type: EnAccountType | string;
    balance: number;
    currency: string;
    label?: string;
    onView?: (id: string) => void;
    onEdit?: (id: string, name: string, type: string) => void;
    onDelete?: (id: string) => void;
    onAlert?: (id: string) => void;
    onReconcile?: (id: string) => void;
    menuOpen: boolean;
    onMenuOpen: (id: string) => void;
    onMenuClose: () => void;
    onDragStart: (id: string) => void;
    onDrop: (id: string) => void;
    onDragEnd: () => void;
}
