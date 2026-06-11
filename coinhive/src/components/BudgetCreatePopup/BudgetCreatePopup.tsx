import { X } from 'lucide-react';
import { useForm } from 'react-hook-form';

import styles from './BudgetCreatePopup.module.css';

interface Props {
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

const BudgetCreatePopup = ({
    handleClose
}: Props) => {
    const {
        register,
        handleSubmit,
        formState: { errors }
    } = useForm<CreateBudgetForm>();

    const onSubmit = (
        data: CreateBudgetForm
    ) => {
        console.log(data);

        handleClose();
    };

    return (
        <div className={styles.overlay}>
            <div className={styles.popup}>
                <div className={styles.header}>
                    <h2>Create Budget</h2>

                    <button
                        onClick={handleClose}
                        className={styles.closeBtn}
                    >
                        <X />
                    </button>
                </div>

                <form
                    className={styles.form}
                    onSubmit={handleSubmit(onSubmit)}
                >
                    <div className={styles.field}>
                        <label>Name</label>

                        <input
                            {...register('name', {
                                required: 'Name is required'
                            })}
                        />

                        {errors.name && (
                            <span className={styles.error}>
                                {errors.name.message}
                            </span>
                        )}
                    </div>

                    <div className={styles.field}>
                        <label>Category</label>

                        <select
                            {...register('categoryId')}
                        >
                            <option>
                                Select Category
                            </option>
                        </select>
                    </div>

                    <div className={styles.field}>
                        <label>Amount</label>

                        <input
                            type="number"
                            {...register('amount', {
                                valueAsNumber: true
                            })}
                        />
                    </div>

                    <div className={styles.field}>
                        <label>Currency</label>

                        <select
                            {...register('currency')}
                        >
                            <option>USD</option>
                            <option>EUR</option>
                            <option>MAD</option>
                        </select>
                    </div>

                    <div className={styles.field}>
                        <label>Period</label>

                        <select
                            {...register('budgetPeriod')}
                        >
                            <option>Weekly</option>
                            <option>Monthly</option>
                            <option>Yearly</option>
                        </select>
                    </div>

                    <div className={styles.field}>
                        <label>Start Date</label>

                        <input
                            type="date"
                            {...register('startDate')}
                        />
                    </div>

                    <div className={styles.field}>
                        <label>End Date</label>

                        <input
                            type="date"
                            {...register('endDate')}
                        />
                    </div>

                    <button
                        className={styles.submitBtn}
                        type="submit"
                    >
                        Create Budget
                    </button>
                </form>
            </div>
        </div>
    );
};

export default BudgetCreatePopup;