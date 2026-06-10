import { useMutation } from "@tanstack/react-query";
import {
    categoriesService,
    type UpdateCategoryParams
} from "../../services/categoriesService";
export function useUpdateCategory() {
    return useMutation({
        mutationFn: (category: UpdateCategoryParams) => categoriesService.UpdateCategory(category),
    })
}