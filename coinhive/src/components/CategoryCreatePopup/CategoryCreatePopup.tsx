import { X } from 'lucide-react';
import { Controller, useForm } from 'react-hook-form';
import styles from './CategoryCreatePopup.module.css';
import type { CreateCategoryParams } from '../../services/categoriesService';
import { useCreateCategory } from '../../hooks/Categories/useCreateCategory';
import Button from '../Button/Button';
import Input from '../Input/Input';
import CustomSelect from '../Select/Select';
import type { CategoryCreatePopupProps } from './types';
import { CATEGORY_TYPE_OPTIONS, DEFAULT_CATEGORY_VALUES } from './constants';

const CategoryCreatePopup = ({ handleClose }: CategoryCreatePopupProps) => {
    const {
        register,
        handleSubmit,
        control,
        formState: { errors },
    } = useForm<CreateCategoryParams>({
        defaultValues: DEFAULT_CATEGORY_VALUES,
    });

    const createCategoryMutation = useCreateCategory();

    const onSubmit = async (data: CreateCategoryParams) => {
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
                        type="button"
                        className={styles.closeBtn}
                        onClick={handleClose}
                        aria-label="Close"
                    >
                        <X size={20} />
                    </button>
                </div>

                <form className={styles.form} onSubmit={handleSubmit(onSubmit)}>
                    <div className={styles.field}>
                        <label className={styles.label}>Name</label>
                        <Input
                            placeholder="e.g. Groceries"
                            {...register('name', {
                                required: 'Name is required',
                            })}
                        />
                        {errors.name && (
                            <span className={styles.error}>
                                {errors.name.message}
                            </span>
                        )}
                    </div>

                    <div className={styles.field}>
                        <label className={styles.label}>Type</label>
                        <Controller
                            control={control}
                            name="type"
                            render={({ field }) => (
                                <CustomSelect
                                    value={field.value}
                                    onChange={field.onChange}
                                    options={CATEGORY_TYPE_OPTIONS}
                                />
                            )}
                        />
                    </div>

                    <Button
                        type="submit"
                        disabled={createCategoryMutation.isPending}
                    >
                        {createCategoryMutation.isPending
                            ? 'Creating...'
                            : 'Create Category'}
                    </Button>
                </form>
            </div>
        </div>
    );
};

export default CategoryCreatePopup;