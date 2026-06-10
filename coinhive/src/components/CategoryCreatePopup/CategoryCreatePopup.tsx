import { X } from 'lucide-react';
import { useForm } from 'react-hook-form';
import styles from './CategoryCreatePopup.module.css';
import type { CreateCategoryParams } from "../../services/categoriesService";
import {useCreateCategory} from "../../hooks/Categories/useCreateCategory";
interface Props {
    handleClose: () => void;
}


const CategoryCreatePopup = ({
    handleClose
}: Props) => {
    const {
        register,
        handleSubmit,
        formState: { errors }
    } = useForm<CreateCategoryParams>({
        defaultValues: {
            name: '',
            type: 'Expense',
            description : '',
        }
    });
    const createCategoryMutation = useCreateCategory();
    const onSubmit = async  (data: CreateCategoryParams) => {
        console.log(data);
        try {
            await createCategoryMutation.mutateAsync(data);
        } catch (err) {
            console.log(err);
        }

        

        handleClose();
    };

    return (
        <div className={styles.overlay}>
            <div className={styles.popup}>
                <div className={styles.header}>
                    <h2>Create Category</h2>

                    <button
                        className={styles.closeBtn}
                        onClick={handleClose}
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
                        <label>Type</label>

                        <select {...register('type')}>
                            <option value="Expense">
                                Expense
                            </option>

                            <option value="Income">
                                Income
                            </option>
                        </select>
                    </div>

                    <button
                        type="submit"
                        className={styles.submitBtn}
                    >
                        Create Category
                    </button>
                </form>
            </div>
        </div>
    );
};

export default CategoryCreatePopup;