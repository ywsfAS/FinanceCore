type Transaction = {
    id: string;
    accountId: string;
    toAccountId?: string;
    categoryId: string;
    amount: number;
    type: "Income" | "Expense" | "Transfer" | "Debt" | "Credit";
    date: string;
    description: string;
};

type Filters = {
    categoryId?: string;
    start?: string;
    end?: string;
    type?: string;
    page?: number;
    pageSize?: number;
};

export function useTransactions(filters: Filters) {
    const [data, setData] = useState < Transaction[] > ([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const query = new URLSearchParams(filters as any).toString();

        fetch(`/api/v1/transactions?${query}`, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem("token")}`,
            },
        })
            .then(res => res.json())
            .then(setData)
            .finally(() => setLoading(false));
    }, [JSON.stringify(filters)]);

    return { data, loading };
}