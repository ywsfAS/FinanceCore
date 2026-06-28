import type { ReactNode } from 'react';

export type CategoryCardActionKey = 'edit' | 'delete';

export interface CategoryMenuAction {
    key: CategoryCardActionKey;
    label: string;
}

export interface CategoryCardProps {
    id: string;
    name: string;
    icon: ReactNode;
    amount: number;
    percentage: number;
    onEdit?: (id: string) => void;
    onDelete?: (id: string) => void;
}