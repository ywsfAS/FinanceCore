type MonthlySummary = {
    accountId: string;
    year: number;
    month: number;
    totalIncome: number;
    totalExpenses: number;
    netSavings: number;
};

export function useMonthlySummary(year: number, month: number) {
    const [data, setData] = useState<MonthlySummary[] | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        if (!year || !month) return;

        const fetchData = async () => {
            try {
                setLoading(true);

                const res = await fetch(
                    `/api/v1/reports/monthly-summary-per-user?year=${year}&month=${month}`,
                    {
                        headers: {
                            Authorization: `Bearer ${localStorage.getItem("token")}`,
                        },
                    }
                );

                if (!res.ok) throw new Error("Failed to fetch summary");

                const json = await res.json();
                setData(json);
            } catch (err: any) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, [year, month]);

    return { data, loading, error };
}