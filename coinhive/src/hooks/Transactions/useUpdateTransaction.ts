export function useUpdateTransaction() {
    const update = async (id: string, payload: any) => {
        await fetch(`/api/v1/transactions/${id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                Authorization: `Bearer ${localStorage.getItem("token")}`,
            },
            body: JSON.stringify(payload),
        });
    };

    return { update };
}