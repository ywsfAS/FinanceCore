import { X } from 'lucide-react';
import { useForm } from 'react-hook-form';
import { useCreateAccount } from '../../hooks/Account/useCreateAccount';
import type { CreateAccountParams } from '../../services/accountService';
import styles from './AccountCreatePopup.module.css';
import Input from "../Input/Input"; 
import Button from "../Button/Button";
import { CURRENCIES, ACCOUNT_TYPES } from "../Accounts/constants";
import CostumSelect from "../Select/Select";

interface AccountCreatePopUpProps {
    handleClose: () => void;
}

const initialCreateAccount: CreateAccountParams = {
    name: "",
    currency: "USD",
    initialBalance: 0,
    type: "cash",
};

const AccountCreatePopup = ({ handleClose }: AccountCreatePopUpProps) => {
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
                        <Input
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
                        <CostumSelect
                            {...register('type', {
                                required: "Account type is required",
                            })}
                            options={ACCOUNT_TYPES}
                        />
                        {errors.type && <div>{errors.type.message}</div>}
                    </div>

                    <div className={styles.field}>
                        <label>Currency</label>
                        <CostumSelect
                            {...register('currency', {
                                required: "Currency is required",
                            })}
                            options={CURRENCIES}
                        />
                        {errors.currency && <div>{errors.currency.message}</div>}
                    </div>
                    <div className={styles.field}>
                        <label>Initial Balance</label>
                        <Input
                            type="number"
                            {...register('initialBalance', {
                                valueAsNumber: true,
                            })}
                        />
                        {errors.initialBalance && (
                            <div>{errors.initialBalance.message}</div>
                        )}
                    </div>

                    <Button
                        type="submit"
                        disabled={isSubmitting || createAccountMutation.isPending}
                    >
                        Create Account
                    </Button>
                </form>
            </div>
        </div>
    );
};

export default AccountCreatePopup;