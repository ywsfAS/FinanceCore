import { X } from 'lucide-react';
import { useForm } from 'react-hook-form';
import Button from '../Button/Button';
import styles from './BudgetCreatePopup.module.css';

interface BudgetCreatePopupProps {
    handleClose: () => void;
}

interface CreateBudgetForm {
    name: string;
    categoryId: string;
    amount: number;
    currency: string;
    budgetPeriod: string;
    startDate: string;
    endDate: string;
}

const BudgetCreatePopup = ({ handleClose }: BudgetCreatePopupProps) => {
    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<CreateBudgetForm>();

    const onSubmit = (data: CreateBudgetForm) => {
        console.log(data);
        handleClose();
    };

    return (
        <div className={styles.overlay}>
            <div className={styles.popup}>
                <div className={styles.header}>
                    <h2>Create Budget</h2>
                    <button className={styles.closeBtn} onClick={handleClose} type="button">
                        <X size={18} />
                    </button>
                </div>

                <form className={styles.form} onSubmit={handleSubmit(onSubmit)}>
                    <div className={styles.field}>
                        <label>Name</label>
                        <input
                            {...register('name', {
                                required: 'Name is required',
                            })}
                        />
                        {errors.name && <span className={styles.error}>{errors.name.message}</span>}
                    </div>

                    <div className={styles.field}>
                        <label>Category</label>
                        <select {...register('categoryId')}>
                            <option value="">Select Category</option>
                            <option value="food">Food</option>
                            <option value="travel">Travel</option>
                            <option value="work">Work</option>
                        </select>
                    </div>

                    <div className={styles.field}>
                        <label>Amount</label>
                        <input
                            type="number"
                            {...register('amount', {
                                valueAsNumber: true,
                            })}
                        />
                    </div>

                    <div className={styles.field}>
                        <label>Currency</label>
                        <select {...register('currency')}>
                            <option value="USD">USD</option>
                            <option value="EUR">EUR</option>
                            <option value="MAD">MAD</option>
                        </select>
                    </div>

                    <div className={styles.field}>
                        <label>Period</label>
                        <select {...register('budgetPeriod')}>
                            <option value="Weekly">Weekly</option>
                            <option value="Monthly">Monthly</option>
                            <option value="Yearly">Yearly</option>
                        </select>
                    </div>

                    <div className={styles.field}>
                        <label>Start Date</label>
                        <input type="date" {...register('startDate')} />
                    </div>

                    <div className={styles.field}>
                        <label>End Date</label>
                        <input type="date" {...register('endDate')} />
                    </div>

                    <div className={styles.actions}>
                        <Button type="button" variant="secondary" onClick={handleClose}>Cancel</Button>
                        <Button type="submit">Create Budget</Button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default BudgetCreatePopup;