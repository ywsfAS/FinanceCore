import { useMutation } from "@tanstack/react-query";
import {
    categoriesService,
    type RemoveCategoryParams
} from "../../services/categoriesService";
export function useRemoveCategory() {
    return useMutation({
        mutationFn: (category: RemoveCategoryParams) => categoriesService.RemoveCategory(category),
    })
}