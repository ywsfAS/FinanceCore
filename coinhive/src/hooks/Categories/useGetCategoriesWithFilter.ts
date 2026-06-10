import { useQuery } from "@tanstack/react-query";
import {
    categoriesService,
    type CategoriesWithFiltersParams 
} from "../../services/categoriesService";

export function useGetCategoriesWithFilters(filters: CategoriesWithFiltersParams) {
    return useQuery({
        queryKey: ["categories-user-filters", filters],

        queryFn: () =>
            categoriesService.getCategoriesWithFilter(filters),

        staleTime: 1000 * 60 * 5,
        placeholderData: (prev) => prev,
    });
}