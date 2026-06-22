import type {AccountType} from "../../pages/Accounts/constants";
export interface AccountCardProps {
    id: string;
    name: string;
    type: AccountType;
    balance: number;
    currency: string;
    label: string;
    onView?: (id: string) => void;
}
