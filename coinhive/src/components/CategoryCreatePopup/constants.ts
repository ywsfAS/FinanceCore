import type { CreateCategoryParams } from '../../services/categoriesService';

export const CATEGORY_TYPE_OPTIONS = [
    { label: 'Expense', value: 'Expense' },
    { label: 'Income', value: 'Income' },
];

export const DEFAULT_CATEGORY_VALUES: CreateCategoryParams = {
    name: '',
    type: 'Expense',
    description: '',
};