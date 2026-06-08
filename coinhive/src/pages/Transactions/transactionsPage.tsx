import styles from './transactionsPage.module.css';
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { useState } from 'react';
import { Calendar } from 'lucide-react';
import { format, toDate } from "date-fns";
import TransactionCard  from "../../components/TransactionCard/TransactionCard";

export interface Transactionfilters {
    fromDate: Date | null;
    toDate: Date | null;
    category: string;
}


const categories = [
    { id: 1, name: 'Food' },
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
        setFilters((prev) => ({...prev,fromDate : date}))
    }
    const handleToDateChange = (date : Date | null) => {
        setFilters((prev) => ({...prev,toDate : date}))
    }
    const handleCategoryChange = (value: string) => {
        setFilters((prev) => ({...prev , category : value}))
    }
    const handleReset = () => {
        setFilters(initialFiltersState);
    }


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
                        format(filters.toDate,"dd/MM/yyyy") : "Select end date"}</button>} />
                    </div>
                    <div className={styles.category}>
                        <div>category</div>
                        <select value={filters.category} onChange={(e) => handleCategoryChange(e.target.value)}>
                            <option value="">Select Category</option>
                            {categories.map(({ id, name }) => <option key={id} value={name}>{name}</option>)}
                        </select>
                    </div>
                    <div className={styles.type }>
                </div>
                </div>
                <div className={styles.right}>
                    <button className={styles.btn}>save</button>
                    <button className={styles.btn} onClick={handleReset}>reset</button>
                </div>
            </div>
            <TransactionCard />



        </div>
    );
}
export default TransactionsPage;
