type NetWorth = {
    netWorth: number;
};

export function useNetWorth() {
    const [data, setData] = useState<NetWorth | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchData = async () => {
            const res = await fetch(`/api/v1/reports/net-worth`, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem("token")}`,
                },
            });

            const json = await res.json();
            setData(json);
            setLoading(false);
        };

        fetchData();
    }, []);

    return { data, loading };
}