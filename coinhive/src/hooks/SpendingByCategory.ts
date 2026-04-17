type CategorySpending = {
    category: string;
    spentAmount: number;
};

export function useSpendingByCategory(
    accountId: string,
    year: number,
    month: number
) {
    const [data, setData] = useState<CategorySpending[] | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        if (!accountId || !year || !month) return;

        const fetchData = async () => {
            try {
                setLoading(true);

                const res = await fetch(
                    `/api/v1/reports/spending-by-category?accountId=${accountId}&year=${year}&month=${month}`,
                    {
                        headers: {
                            Authorization: `Bearer ${localStorage.getItem("token")}`,
                        },
                    }
                );

                if (!res.ok) throw new Error("Failed to fetch category spending");

                const json = await res.json();
                setData(json);
            } catch (err: any) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, [accountId, year, month]);

    return { data, loading, error };
}