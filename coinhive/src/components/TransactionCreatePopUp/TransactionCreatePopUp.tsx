import styles from "./TransactionCreatePopUp.module.css";
import { useForm, type SubmitHandler } from "react-hook-form";

import { useUserAccountsOptions } from "../../hooks/User/useUserAccountsOptions";
import { useUserCategoriesOptions } from "../../hooks/User/useUserCategoriesOptions";
import { useCreateTransaction } from "../../hooks/Transactions/useCreateTransaction";
import Button from "../Button/Button";

import {
    TransactionType,
    type CreateTransactionParams,
} from "../../services/transactionService";
import { CREATE_TRANSACTION_COPY } from "./constants";

interface TransactionCreatePopUpProps {
    handleClose: () => void;
}

const TransactionCreatePopUp = ({ handleClose }: TransactionCreatePopUpProps) => {
    const {
        register,
        handleSubmit,
        formState: { errors, isSubmitting },
    } = useForm<CreateTransactionParams>({
        defaultValues: {
            transactionDate: new Date().toISOString()
        }
    });

    const {
        data: categoriesData,
        isLoading: isCategoriesLoading,
        error: categoryError,
        isError: isCategoryError,
    } = useUserCategoriesOptions();

    const {
        data: accountsData,
        isLoading: isAccountsLoading,
        error: accountsError,
        isError: isAccountError,
    } = useUserAccountsOptions();


    const createTransactionMutation = useCreateTransaction();
    const onSubmit: SubmitHandler<CreateTransactionParams> = async (data) => {
        console.log(data);
        try {
            await createTransactionMutation.mutateAsync(data);
            handleClose();
        } catch (err) {
            console.log(err);
        }

    };

    const accounts = accountsData ?? [];
    const categories = categoriesData ?? [];

    return (
        <div className={styles.overlay}>
            <form
                className={styles.popUp}
                onSubmit={handleSubmit(onSubmit)}
            >
                <div className={styles.header}>
                    <h1 className={styles.title}>{CREATE_TRANSACTION_COPY.title}</h1>
                    <p className={styles.description}>{CREATE_TRANSACTION_COPY.description}</p>
                </div>
                {(isCategoriesLoading || isAccountsLoading) && <p>Loading account and category options...</p>}
                {(isCategoryError || isAccountError) && <p className={styles.error}>{accountsError?.message} {categoryError?.message}</p>}

                {/* Account */}
                <div className={styles.field}>
                    <label htmlFor="accountId">{CREATE_TRANSACTION_COPY.fields.account.label}</label>
                    <p>{CREATE_TRANSACTION_COPY.fields.account.description}</p>

                    <select
                        id="accountId"
                        {...register("accountId", {
                            required: "Account is required",
                        })}
                    >
                        <option value="">
                            Select an account
                        </option>

                        {accounts.map((account) => (
                            <option
                                key={account.id}
                                value={account.id}
                            >
                                {account.name}
                            </option>
                        ))}
                    </select>

                    {errors.accountId && (
                        <span className={styles.error}>
                            {errors.accountId.message}
                        </span>
                    )}
                </div>

                {/* Category */}
                <div className={styles.field}>
                    <label htmlFor="categoryId">{CREATE_TRANSACTION_COPY.fields.category.label}</label>
                    <p>{CREATE_TRANSACTION_COPY.fields.category.description}</p>

                    <select
                        id="categoryId"
                        {...register("categoryId", {
                            required: "Category is required",
                        })}
                    >
                        <option value="">
                            Select a category
                        </option>

                        {categories.map((category) => (
                            <option
                                key={category.id}
                                value={category.id}
                            >
                                {category.name}
                            </option>
                        ))}
                    </select>

                    {errors.categoryId && (
                        <span className={styles.error}>
                            {errors.categoryId.message}
                        </span>
                    )}
                </div>

                {/* Transaction Type */}
                <div className={styles.field}>
                    <label htmlFor="type">
                        {CREATE_TRANSACTION_COPY.fields.type.label}
                    </label>
                    <p>{CREATE_TRANSACTION_COPY.fields.type.description}</p>

                    <select
                        id="type"
                        {...register("type", {
                            required:
                                "Transaction type is required",
                        })}
                    >
                        <option value="">
                            Select transaction type
                        </option>

                        {Object.values(TransactionType).map(
                            (type) => (
                                <option
                                    key={type}
                                    value={type}
                                >
                                    {type}
                                </option>
                            )
                        )}
                    </select>

                    {errors.type && (
                        <span className={styles.error}>
                            {errors.type.message}
                        </span>
                    )}
                </div>

                {/* Amount */}
                <div className={styles.field}>
                    <label htmlFor="amount">{CREATE_TRANSACTION_COPY.fields.amount.label}</label>
                    <p>{CREATE_TRANSACTION_COPY.fields.amount.description}</p>

                    <input
                        id="amount"
                        type="number"
                        step="0.01"
                        placeholder="Enter amount"
                        {...register("amount", {
                            required: "Amount is required",
                            valueAsNumber: true,
                            min: {
                                value: 0.01,
                                message:
                                    "Amount must be greater than 0",
                            },
                        })}
                    />

                    {errors.amount && (
                        <span className={styles.error}>
                            {errors.amount.message}
                        </span>
                    )}
                </div>

                {/* Description */}
                <div className={styles.field}>
                    <label htmlFor="description">
                        {CREATE_TRANSACTION_COPY.fields.description.label}
                    </label>
                    <p>{CREATE_TRANSACTION_COPY.fields.description.description}</p>

                    <input
                        id="description"
                        type="text"
                        placeholder="Enter description"
                        {...register("description")}
                    />
                </div>

                <div className={styles.actions}>
                    <Button
                        type="button"
                        variant="secondary"
                        onClick={handleClose}
                    >
                        Cancel
                    </Button>

                    <Button
                        type="submit"
                        variant="primary"
                        disabled={isSubmitting || createTransactionMutation.isPending}
                    >
                        {isSubmitting || createTransactionMutation.isPending
                            ? "Creating..."
                            : CREATE_TRANSACTION_COPY.submit}
                    </Button>
                </div>
            </form>
        </div>
    );
};

export default TransactionCreatePopUp;