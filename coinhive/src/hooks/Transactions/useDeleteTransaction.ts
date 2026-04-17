export function useDeleteTransaction() {
    const remove = async (id: string) => {
        await fetch(`/api/v1/transactions/${id}`, {
            method: "DELETE",
            headers: {
                Authorization: `Bearer ${localStorage.getItem("token")}`,
            },
        });
    };

    return { remove };
}