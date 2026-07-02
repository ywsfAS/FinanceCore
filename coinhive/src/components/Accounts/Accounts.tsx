import styles from "./Accounts.module.css";
import { useState } from "react";

import AccountCard from "../AccountCard/AccountCard";
import AccountCreatePopUp from "../AccountCreatePopup/AccountCreatePopup";
import Input from "../Input/Input";
import CustomSelect from "../Select/Select";

import { useGetAccountsWithFilters } from "../../hooks/Account/useGetAccountWithFilters";

import type { GetAccountWithFiltersParams } from "../../services/accountService";

import {
    ACCOUNT_TYPES,
    CURRENCIES,
    INITIAL_FILTERS,
    MOCK_ACCOUNTS,
    HEADER,
} from "./constants";
import SectionHeader from "../SectionHeader/SectionHeader";

const Accounts = () => {
    const [open, setOpen] = useState(false);

    const [filters, setFilters] =
        useState<GetAccountWithFiltersParams>(INITIAL_FILTERS);

    const { data, isLoading, isError, error } =
        useGetAccountsWithFilters(filters);

    const handleClose = () => {
        setOpen((prev) => !prev);
    };

    const updateFilter = (
        key: keyof GetAccountWithFiltersParams,
        value: string
    ) => {
        setFilters((prev) => ({
            ...prev,
            [key]: value,
        }));
    };

    if (isLoading) return <div>Loading...</div>;

    //if (isError) return <div>{error.message}</div>;

    const accounts = data?.length ? data : MOCK_ACCOUNTS;

    return (
        <div className={styles.wrapper}>
            <SectionHeader title={HEADER.title} subtitle={HEADER.subtitle} btnName={HEADER.btnName} handler={handleClose} />
            <div className={styles.filterSection}>
                <div className={styles.searchContainer}>
                    <Input
                        placeholder="Search account..."
                        value={filters.name}
                        onChange={(e) =>
                            updateFilter("name", e.target.value)
                        }
                    />
                </div>

                <CustomSelect
                    value={filters.type}
                    onChange={(value) =>
                        updateFilter("type", value)
                    }
                    options={ACCOUNT_TYPES}
                />
                <CustomSelect
                    value={filters.currency}
                    onChange={(value) =>
                        updateFilter("currency", value)
                    }
                    options={CURRENCIES}
                />
            </div>

            <div className={styles.accountsGrid}>
                {accounts.map((account) => (
                    <AccountCard
                        key={account.id}
                        id={account.id}
                        name={account.name}
                        type={account.type}
                        balance={account.balance}
                        currency={account.currency}
                        label={account.label}
                    />
                ))}
            </div>

            {open && (
                <AccountCreatePopUp
                    handleClose={handleClose}
                />
            )}
        </div>
    );
};

export default Accounts;