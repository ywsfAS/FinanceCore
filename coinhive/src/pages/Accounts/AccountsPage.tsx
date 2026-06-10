import styles from './AccountsPage.module.css';
import { Plus, Search } from 'lucide-react';
import { useState } from 'react';
import AccountCard from '../../components/AccountCard/AccountCard';
import AccountCreatePopUp from "../../components/AccountCreatePopup/AccountCreatePopup";
import { useGetAccountsWithFilters } from "../../hooks/Account/useGetAccountWithFilters";
import type {GetAccountWithFiltersParams } from "../../services/accountService";
export enum AccountType {
    checking = "checking",
    credit = "credit",
    savings = "savings",
    cash = "cash"
}
const staticAccounts = [
    {id : 1 , name : "account1" , type : AccountType.checking , balance : 100 , currency : "USD"},
    {id : 2 , name : "account2" , type : AccountType.checking , balance : 200 , currency : "USD"},
    {id : 3 , name : "account3" , type : AccountType.checking , balance : 300 , currency : "USD"},
    {id : 4 , name : "account4" , type : AccountType.checking , balance : 400 , currency : "USD"}
];

const AccountsPage = () => {
    const initialFilters: GetAccountWithFiltersParams = {
        name: "",
        type: "",
        currency: "",

    }
    const [open, setOpen] = useState(false);
    const handleClose = () => {
        setOpen((prev) => !prev);
    }
    const [filters, setFilters] = useState<GetAccountWithFiltersParams>(initialFilters);

    // get user accounts
    const { data, isLoading, isError, error } = useGetAccountsWithFilters(filters);
    if (isLoading) return <div>loading...</div>
    if (isError) return <div>{error.message}</div>

    const accounts = data ?? staticAccounts;

    const onNameChangeHandler = (value: string) => {
        setFilters((prev) => ({...prev, name : value  }));
    };

    const onTypeChangeHandler = (value: string) => {
        setFilters((prev) => ({...prev, type : value  }));
    };

    const onCurrencyChangeHandler = (value: string) => {
        setFilters((prev) => ({...prev, currency : value  }));
    };
    
    return (
        <div className={styles.wrapper}>
            <div className={styles.header}>
                <div>
                    <h1>Accounts</h1>
                    <p>Manage and monitor your financial accounts</p>
                </div>

                <button className={styles.btn} onClick={handleClose}>
                    <Plus size={18} />
                    New Account
                </button>
            </div>

            <div className={styles.filterSection}>
                <div className={styles.searchContainer}>
                    <Search size={18} />
                    <input
                        type="text"
                        placeholder="Search account..."
                        value={filters.name}
                        onChange={(e) => onNameChangeHandler(e.target.value)}
                    />
                </div>

                <select
                    value={filters.type}
                    onChange={(e) => onTypeChangeHandler(e.target.value)}
                >
                    <option value="">All Types</option>
                    <option value="checking">Checking</option>
                    <option value="savings">Savings</option>
                    <option value="cash">Cash</option>
                </select>

                <select
                    value={filters.currency}
                    onChange={(e) => onCurrencyChangeHandler(e.target.value)}
                >
                    <option value="">All Currencies</option>
                    <option value="USD">USD</option>
                    <option value="EUR">EUR</option>
                    <option value="MAD">MAD</option>
                </select>
            </div>

            <div className={styles.accountsGrid}>
                {accounts.map((acc) => <AccountCard id={acc.id} key={acc.id} name={acc.name} type={acc.type} balance={acc.balance} currency={acc.currency} />)}
            </div>
            {open && <AccountCreatePopUp handleClose={handleClose} /> }
        </div>
    );
};

export default AccountsPage;