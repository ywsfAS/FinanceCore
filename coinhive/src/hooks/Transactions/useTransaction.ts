export function useTransaction(id: string) {
    const [data, setData] = useState<Transaction | null>(null);

    useEffect(() => {
        if (!id) return;

        fetch(`/api/v1/transactions/${id}`, {
            headers: {
                Authorization: `Bearer ${localStorage.getItem("token")}`,
            },
        })
        .then(res => res.json())
        .then(setData);
    }, [id]);

    return { data };
}