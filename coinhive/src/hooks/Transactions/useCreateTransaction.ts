export function useCreateTransaction() {
    const [loading, setLoading] = useState(false);

    const create = async (payload: {
        accountId: string;
        categoryId: string;
        type: string;
        amount: number;
        description: string;
        transactionDate: string;
    }) => {
        setLoading(true);

        const res = await fetch(`/api/v1/transactions`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Authorization: `Bearer ${localStorage.getItem("token")}`,
            },
            body: JSON.stringify(payload),
        });

        setLoading(false);

        if (!res.ok) throw new Error("Failed to create transaction");

        return res.json();
    };

    return { create, loading };
}