import type { EnAccountType } from "../../entities/Account";

export interface AccountCardProps {
    id: string;
    name: string;
    type: EnAccountType | string;
    balance: number;
    currency: string;
    label: string;
    onView?: (id: string) => void;
}
