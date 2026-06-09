import styles from "./TransactionCreatePopUp.module.css";
import { useForm, type SubmitHandler } from "react-hook-form";

import { useUserAccountsOptions } from "../../hooks/User/useUserAccountsOptions";
import { useUserCategoriesOptions } from "../../hooks/User/useUserCategoriesOptions";
import {useCreateTransaction} from "../../hooks/Transactions/useCreateTransaction";

import {
    TransactionType,
    type CreateTransactionParams,
} from "../../services/transactionService";

const staticCategories = [
    { id: 1, name: "Food" },
    { id: 2, name: "Sport" },
    { id: 3, name: "Transportation" },
];

const staticAccounts = [
    { id: 1, name: "Account 1" },
    { id: 2, name: "Account 2" },
    { id: 3, name: "Account 3" },
];

const TransactionCreatePopUp = ({handleClose}) => {
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

        } catch (err) {
            console.log(err);
        }

    };

    if (isCategoriesLoading || isAccountsLoading) {
        return <div>Loading...</div>;
    }

    if (isCategoryError || isAccountError) {
        return (
            <div>
                {accountsError?.message} {categoryError?.message}
            </div>
        );
    }

    const accounts = accountsData ?? staticAccounts;
    const categories = categoriesData ?? staticCategories;

    return (
        <div className={styles.overlay}>
            <form
                className={styles.popUp}
                onSubmit={handleSubmit(onSubmit)}
            >
                <h1 className={styles.title}>Create Transaction</h1>

                {/* Account */}
                <div className={styles.field}>
                    <label htmlFor="accountId">Account</label>

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
                    <label htmlFor="categoryId">Category</label>

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
                        Transaction Type
                    </label>

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
                    <label htmlFor="amount">Amount</label>

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
                        Description
                    </label>

                    <input
                        id="description"
                        type="text"
                        placeholder="Enter description"
                        {...register("description")}
                    />
                </div>

                <div className={styles.actions}>
                    <button
                        type="button"
                        className={styles.cancelBtn}
                        onClick={handleClose}
                    >
                        Cancel
                    </button>

                    <button
                        type="submit"
                        className={styles.submitBtn}
                        disabled={isSubmitting}
                    >
                        {isSubmitting
                            ? "Creating..."
                            : "Create Transaction"}
                    </button>
                </div>
            </form>
        </div>
    );
};

export default TransactionCreatePopUp;