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
import { CATEGORIES, HEADER } from "./constants";
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
    // pop up state
    const [open, setOpen] = useState<boolean>(false);
    const handleClose = () => {
        setOpen((prev) => !prev);
    }
    const [filters, setFilters] = useState<Transactionfilters>(initialFiltersState);
    const handleFromDateChange = (date: Date | null) => {
        setFilters((prev) => ({ ...prev, fromDate: date }))
        setPage(1);
    }
    const handleToDateChange = (date: Date | null) => {
        setFilters((prev) => ({ ...prev, toDate: date }))
        setPage(1);
    }
    const handleCategoryChange = (value: string) => {
        setFilters((prev) => ({ ...prev, category: value }))
        setPage(1);


    }
    const handleReset = () => {
        setFilters(initialFiltersState);
        setPage(1);
    }
    // pagination state
    const [page, setPage] = useState<number>(1);
    const pageSize = 3;
    const handleNextPage = () => {
        setPage((prev) => prev + 1);
    };
    const handlePrevPage = () => {
        setPage((prev) => Math.max(prev - 1, 1));
    }
    // categories options
    const { data, isLoading, error, isError } = useUserCategoriesOptions();
    //if (isLoading) return <div>loading....</div>;
    //if (isError) return <div>{error.message}</div>;
    const categories = data ?? CATEGORIES;

    // get selected category id
    const cat = categories.find(cat => cat.value.toLowerCase() === filters.category);
    const id = cat?.id;


    return (
        <div className={styles.wrapper}>
            <SectionHeader title={HEADER.title} subtitle={HEADER.subtitle} btnName={HEADER.btnName} handler={() => setOpen((prev) => !prev)} />
            <div className={styles.filterSection}>
                <div className={styles.left}>
                    <DatePicker selected={filters.fromDate} onChange={handleFromDateChange} customInput={<button className={styles.datePickerBtn}><Calendar />{filters.fromDate
                        ? format(filters.fromDate, "dd/MM/yyyy")
                        : "Select start date"}</button>} />
                    <DatePicker selected={filters.toDate} onChange={handleToDateChange} customInput={<button className={styles.datePickerBtn}><Calendar />{filters.toDate ?
                        format(filters.toDate, "dd/MM/yyyy") : "Select end date"}</button>} />
                    <CostumSelect value={filters.category} options={CATEGORIES} onChange={(e) => handleCategoryChange(e.target.value)} />
                </div>
                <div className={styles.right}>
                    <Button  >save</Button>
                    <Button variant='secondary' onClick={handleReset}>reset</Button>
                </div>
            </div>
            <TransactionCard Page={page} PageSize={pageSize} Start={filters.fromDate} End={filters.toDate} CategoryId={id ?? ""} />
            <div className={styles.paginationBtnContainer}>
                <Button onClick={handleNextPage} >{`next page ${page + 1}`}</Button>
                <Button onClick={handlePrevPage} >{`previous page ${page - 1}`}</Button>
            </div>
            {open && <TransactionCreatePopUp handleClose={handleClose} />}



        </div>
    );
}
export default Transactions;
