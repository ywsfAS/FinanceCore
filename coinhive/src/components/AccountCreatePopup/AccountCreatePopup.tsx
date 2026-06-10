import styles from './AccountCreatePopup.module.css';
import { X } from 'lucide-react';
import { useForm } from 'react-hook-form';
import type { CreateAccountParams } from '../../services/accountService';
import { useCreateAccount } from '../../hooks/Account/useCreateAccount';

interface Props {
    handleClose: () => void;
}

const initialCreateAccount: CreateAccountParams = {
    name: "",
    currency: "USD",
    initialBalance: 0,
    type: "cash",
};

const AccountCreatePopup = ({ handleClose }: Props) => {
    const {
        register,
        handleSubmit,
        formState: { errors, isSubmitting },
    } = useForm<CreateAccountParams>({
        defaultValues: initialCreateAccount,
    });

    const createAccountMutation = useCreateAccount();

    const onSubmit = async (data: CreateAccountParams) => {
        try {
            await createAccountMutation.mutateAsync(data);
            handleClose();
        } catch (err) {
            if (err instanceof Error) {
                console.error(err.message);
            } else {
                console.error(err);
            }
        }
    };

    return (
        <div className={styles.overlay}>
            <div className={styles.popup}>
                <div className={styles.header}>
                    <h2>Create Account</h2>

                    <button
                        className={styles.closeBtn}
                        onClick={handleClose}
                        type="button"
                    >
                        <X />
                    </button>
                </div>

                <form className={styles.form} onSubmit={handleSubmit(onSubmit)}>

                    
                    <div className={styles.field}>
                        <label>Name</label>
                        <input
                            {...register('name', {
                                required: "Account name is required",
                                maxLength: {
                                    value: 10,
                                    message: "Max length is 10 characters",
                                },
                            })}
                        />
                        {errors.name && <div>{errors.name.message}</div>}
                    </div>

                    <div className={styles.field}>
                        <label>Type</label>
                        <select
                            {...register('type', {
                                required: "Account type is required",
                            })}
                        >
                            <option value="checking">Checking</option>
                            <option value="savings">Savings</option>
                            <option value="cash">Cash</option>
                        </select>
                        {errors.type && <div>{errors.type.message}</div>}
                    </div>

                    <div className={styles.field}>
                        <label>Currency</label>
                        <select
                            {...register('currency', {
                                required: "Currency is required",
                            })}
                        >
                            <option value="USD">USD</option>
                            <option value="EUR">EUR</option>
                            <option value="MAD">MAD</option>
                        </select>
                        {errors.currency && <div>{errors.currency.message}</div>}
                    </div>
                    <div className={styles.field}>
                        <label>Initial Balance</label>
                        <input
                            type="number"
                            {...register('initialBalance', {
                                valueAsNumber: true,
                            })}
                        />
                        {errors.initialBalance && (
                            <div>{errors.initialBalance.message}</div>
                        )}
                    </div>

                    <button
                        type="submit"
                        className={styles.submitBtn}
                        disabled={isSubmitting || createAccountMutation.isPending}
                    >
                        Create Account
                    </button>
                </form>
            </div>
        </div>
    );
};

export default AccountCreatePopup;