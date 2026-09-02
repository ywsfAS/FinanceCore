import styles from './transactions.module.css';
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { useState } from 'react';
import { Calendar } from 'lucide-react';
import { format } from "date-fns";
import TransactionCard from "../TransactionCard/TransactionCard";
import { useUserCategoriesOptions } from '../../hooks/User/useUserCategoriesOptions';
import TransactionCreatePopUp from "../TransactionCreatePopUp/TransactionCreatePopUp";
import Button from "../Button/Button";
import { HEADER } from "./constants";
import CostumSelect from "../Select/Select";
import SectionHeader from '../SectionHeader/SectionHeader';

export interface Transactionfilters {
    fromDate: Date | null | undefined;
    toDate: Date | null | undefined;
    category: string;
}

const Transactions = () => {
    const initialFiltersState: Transactionfilters = {
        fromDate: null,
        toDate: null,
        category: ""
    };

    const [open, setOpen] = useState<boolean>(false);
    const [filters, setFilters] = useState<Transactionfilters>(initialFiltersState);
    const [appliedFilters, setAppliedFilters] = useState<Transactionfilters>(initialFiltersState);
    const [page, setPage] = useState(1);
    const [hasNextPage, setHasNextPage] = useState(true);
    const [isPageLoading, setIsPageLoading] = useState(false);
    const pageSize = 3;

    const handleClose = () => {
        setOpen((prev) => !prev);
    };

    const handleFromDateChange = (date: Date | null) => {
        setFilters((prev) => ({ ...prev, fromDate: date }));
    };

    const handleToDateChange = (date: Date | null) => {
        setFilters((prev) => ({ ...prev, toDate: date }));
    };

    const handleCategoryChange = (value: string) => {
        setFilters((prev) => ({ ...prev, category: value }));
    };

    const handleReset = () => {
        setFilters(initialFiltersState);
        setAppliedFilters(initialFiltersState);
        setPage(1);
        setHasNextPage(true);
    };

    const handleApply = () => {
        setAppliedFilters(filters);
        setPage(1);
        setHasNextPage(true);
    };

    const handleNextPage = () => {
        if (hasNextPage && !isPageLoading) setPage((currentPage) => currentPage + 1);
    };

    const handlePreviousPage = () => {
        setPage((currentPage) => Math.max(currentPage - 1, 1));
    };

    const { data } = useUserCategoriesOptions();
    const categories = Array.isArray(data)
        ? data.map((category) => ({ id: category.id, label: category.name, value: category.id }))
        : [];
    const categoryId = appliedFilters.category;

    return (
        <div className={styles.wrapper}>
            <SectionHeader
                title={HEADER.title}
                subtitle={HEADER.subtitle}
                btnName={HEADER.btnName}
                handler={handleClose}
            />

            <div className={styles.filterSection}>
                <div className={styles.left}>
                    <div className={styles.filterField}>
                        <DatePicker
                            selected={filters.fromDate}
                            onChange={handleFromDateChange}
                            wrapperClassName={styles.datePickerWrapper}
                            popperClassName={styles.datePickerPopper}
                            portalId="root"
                            popperPlacement="bottom-start"
                            customInput={<button className={styles.datePickerBtn}><Calendar size={16} />{filters.fromDate ? format(filters.fromDate, "dd/MM/yyyy") : "Select start date"}</button>}
                        />
                    </div>
                    <div className={styles.filterField}>
                        <DatePicker
                            selected={filters.toDate}
                            onChange={handleToDateChange}
                            wrapperClassName={styles.datePickerWrapper}
                            popperClassName={styles.datePickerPopper}
                            portalId="root"
                            popperPlacement="bottom-start"
                            customInput={<button className={styles.datePickerBtn}><Calendar size={16} />{filters.toDate ? format(filters.toDate, "dd/MM/yyyy") : "Select end date"}</button>}
                        />
                    </div>
                    <div className={styles.filterField}>
                        <CostumSelect value={filters.category} options={[{ label: "All Categories", value: "" }, ...categories]} onChange={handleCategoryChange} variant="secondary" />
                    </div>
                </div>

                <div className={styles.right}>
                    <Button type="button" onClick={handleApply}>Apply</Button>
                    <Button type="button" variant='secondary' onClick={handleReset}>Reset</Button>
                </div>
            </div>
            <div className={styles.listContainer}>
                <TransactionCard
                    filters={{
                        Page: page,
                        PageSize: pageSize,
                        Start: appliedFilters.fromDate,
                        End: appliedFilters.toDate,
                        CategoryId: categoryId,
                    }}
                    onPageAvailabilityChange={setHasNextPage}
                    onLoadingChange={setIsPageLoading}
                />
                <div className={styles.paginationBtnContainer}>
                    <Button type="button" size="small" onClick={handlePreviousPage} disabled={page === 1 || isPageLoading}>Previous</Button>
                    <Button type="button" size="small" onClick={handleNextPage} disabled={!hasNextPage || isPageLoading}>Next</Button>
                </div>

            </div>
            {open && <TransactionCreatePopUp handleClose={handleClose} />}
        </div>
    );
};

export default Transactions;
