import type { CategoryMenuAction } from './types';

export const CURRENCY_LOCALE = 'en-US';
export const CURRENCY_CODE = 'USD';

export const MIN_PERCENTAGE = 0;
export const MAX_PERCENTAGE = 100;

export const CATEGORY_MENU_ACTIONS: CategoryMenuAction[] = [
    { key: 'edit', label: 'Edit category' },
    { key: 'delete', label: 'Delete category' },
];

export const clampPercentage = (value: number): number =>
    Math.min(MAX_PERCENTAGE, Math.max(MIN_PERCENTAGE, value));