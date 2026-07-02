import {
    UtensilsCrossed,
    Car,
    ShoppingBag
} from 'lucide-react';
import type { CategoryCardProps } from './types';
import { Currency } from '../types';

const ICON_SIZE = 20;

export type CategoryCardSeed = Omit<CategoryCardProps, 'onEdit' | 'onDelete'>;

export const CATEGORY_CARDS: CategoryCardSeed[] = [
    {
        id: 'food-dining',
        name: 'Food & Dining',
        icon: <UtensilsCrossed size={ICON_SIZE} />,
        amount: 124.8,
        currency: Currency.EUR,
        percentage: 32,
    },
    {
        id: 'transportation',
        name: 'Transportation',
        icon: <Car size={ICON_SIZE} />,
        amount: 70.2,
        currency: Currency.EUR,
        percentage: 18,
    },
    {
        id: 'shopping',
        name: 'Shopping',
        icon: <ShoppingBag size={ICON_SIZE} />,
        amount: 58.5,
        currency: Currency.EUR,
        percentage: 15,
    },
];
export const initialCategoryFilter: CategoriesWithFiltersParams = {
    name: "",
    type: "",
    page: 1,
    pageSize: 10,
}
export const HEADER = {
    title: "Categories",
    subtitle: "Manage your income and expense categorie",
    btnName: "Add Category",
}