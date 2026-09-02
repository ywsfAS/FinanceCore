import { X } from 'lucide-react';
import { useForm, useWatch } from 'react-hook-form';
import { useCreateAccount } from '../../hooks/Account/useCreateAccount';
import type { CreateAccountParams } from '../../services/accountService';
import styles from './AccountCreatePopup.module.css';
import Input from "../Input/Input";
import Button from "../Button/Button";
import CostumSelect from "../Select/Select";
import { CREATE_ACCOUNT_COPY, CREATE_ACCOUNT_CURRENCIES, CREATE_ACCOUNT_TYPES, INITIAL_CREATE_ACCOUNT } from "./constants";

interface AccountCreatePopUpProps {
    handleClose: () => void;
}

const AccountCreatePopup = ({ handleClose }: AccountCreatePopUpProps) => {
    const {
        register,
        handleSubmit,
        control,
        setValue,
        formState: { errors, isSubmitting },
    } = useForm<CreateAccountParams>({
        defaultValues: INITIAL_CREATE_ACCOUNT,
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
    const selectedType = useWatch({ control, name: "type" });
    const selectedCurrency = useWatch({ control, name: "currency" });

    return (
        <div className={styles.overlay}>
            <div className={styles.popup}>
                <div className={styles.header}>
                    <div>
                        <h2>{CREATE_ACCOUNT_COPY.title}</h2>
                        <p className={styles.description}>{CREATE_ACCOUNT_COPY.description}</p>
                    </div>

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
                        <label>{CREATE_ACCOUNT_COPY.fields.name.label}</label>
                        <p>{CREATE_ACCOUNT_COPY.fields.name.description}</p>
                        <Input
                            placeholder={CREATE_ACCOUNT_COPY.fields.name.placeholder}
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
                        <label>{CREATE_ACCOUNT_COPY.fields.type.label}</label>
                        <p>{CREATE_ACCOUNT_COPY.fields.type.description}</p>
                        <CostumSelect
                            {...register('type', {
                                required: "Account type is required",
                            })}
                            options={CREATE_ACCOUNT_TYPES}
                            placeholder={CREATE_ACCOUNT_COPY.fields.type.placeholder}
                            variant="secondary"
                            onChange={(value) => setValue("type", value)}
                            value={selectedType}
                        />
                        {errors.type && <div>{errors.type.message}</div>}
                    </div>

                    <div className={styles.field}>
                        <label>{CREATE_ACCOUNT_COPY.fields.currency.label}</label>
                        <p>{CREATE_ACCOUNT_COPY.fields.currency.description}</p>
                        <CostumSelect
                            {...register('currency', {
                                required: "Currency is required",
                            })}
                            onChange={(value) => setValue("currency", value)}
                            options={CREATE_ACCOUNT_CURRENCIES}
                            placeholder={CREATE_ACCOUNT_COPY.fields.currency.placeholder}
                            variant="secondary"
                            value={selectedCurrency}
                        />
                        {errors.currency && <div>{errors.currency.message}</div>}
                    </div>
                    <div className={styles.field}>
                        <label>{CREATE_ACCOUNT_COPY.fields.initialBalance.label}</label>
                        <p>{CREATE_ACCOUNT_COPY.fields.initialBalance.description}</p>
                        <Input
                            type="number"
                            placeholder={CREATE_ACCOUNT_COPY.fields.initialBalance.placeholder}
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
                        {CREATE_ACCOUNT_COPY.submit}
                    </Button>
                </form>
            </div>
        </div>
    );
};

export default AccountCreatePopup;