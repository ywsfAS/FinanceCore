import { useMutation } from "@tanstack/react-query";
import {
    categoriesService, 
    type CreateCategoryParams
} from "../../services/categoriesService";
export function useCreateCategory() {
    return useMutation({
        mutationFn: (category: CreateCategoryParams) => categoriesService.CreateCategory(category),
    })
}