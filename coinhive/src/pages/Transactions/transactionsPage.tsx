import styles from './transactionsPage.module.css';
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { useState } from 'react';
import { Calendar } from 'lucide-react';
import { format} from "date-fns";
import TransactionCard  from "../../components/TransactionCard/TransactionCard";
import { useUserCategoriesOptions } from '../../hooks/User/useUserCategoriesOptions';

export interface Transactionfilters {
    fromDate: Date | null | undefined;
    toDate: Date | null | undefined;
    category: string;
}


const categoriesStatic = [
    { id: 1, name: 'food' },
    { id: 2, name: 'transport' },
    { id: 3, name: 'salary' },
    { id: 4, name: 'gym' },

];
const TransactionsPage = () => {
    const initialFiltersState: Transactionfilters = {
        fromDate: null,
        toDate: null,
        category: ""
    }; 
    const [filters, setFilters] = useState<Transactionfilters>(initialFiltersState);
    const handleFromDateChange = (date : Date | null) => {
        setFilters((prev) => ({ ...prev, fromDate: date }))
        setPage(1);
    }
    const handleToDateChange = (date : Date | null) => {
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
        setPage((prev) => Math.max(prev - 1,1));  
    }
    // categories options
    const {data,isLoading,error,isError} = useUserCategoriesOptions();
    if (isLoading) return <div>loading....</div>;
    if (isError) return <div>{error.message}</div>;
    const categories = data ?? categoriesStatic;
   
    // get selected category id
    const cat = data.find(cat => cat.name.toLowerCase() === filters.category);
    const id = cat?.id
  
    return (
        <div className={styles.wrapper}>
            <div className={styles.header}>
                <div className={styles.description}>
                    <h1 className={styles.title}>Transactions</h1>
                    <p>Track and manage all your financial activity</p>
                </div>
                <button className={styles.btn}>+ new transaction</button>
            </div>
            <div className={styles.filterSection}>
                <div className={styles.left}>

                    <div className={styles.from}>
                        <div>from</div>
                        <DatePicker selected={filters.fromDate} onChange={handleFromDateChange} customInput={<button className={styles.datePickerBtn}><Calendar />{filters.fromDate
                            ? format(filters.fromDate, "dd/MM/yyyy")
                            : "Select start date"}</button>} />
                    </div>
                    <div className={styles.to}>
                        <div>to</div>
                        <DatePicker selected={filters.toDate} onChange={handleToDateChange} customInput={<button className={styles.datePickerBtn}><Calendar />{filters.toDate ?
                            format(filters.toDate, "dd/MM/yyyy") : "Select end date"}</button>} />
                    </div>
                    <div className={styles.category}>
                        <div>category</div>
                        <select value={filters.category} onChange={(e) => handleCategoryChange(e.target.value)}>
                            <option value="">Select Category</option>
                            {categories.map(({ id, name }) => <option key={id} value={name.toLowerCase()}>{name.toLowerCase()}</option>)}
                        </select>
                    </div>
                    <div className={styles.type}>
                    </div>
                </div>
                <div className={styles.right}>
                    <button className={styles.btn}>save</button>
                    <button className={styles.btn} onClick={handleReset}>reset</button>
                </div>
            </div>
            <TransactionCard Page={page} PageSize={pageSize} Start={filters.fromDate} End={filters.toDate} CategoryId={id ?? ""} />
            <div className={styles.paginationBtnContainer}>
                <button onClick={handleNextPage} className={styles.btn}>{`next page ${page + 1}`}</button>
                <button onClick={handlePrevPage} className={styles.btn}>{`previous page ${page - 1}`}</button>
            </div>



        </div>
    );
}
export default TransactionsPage;
