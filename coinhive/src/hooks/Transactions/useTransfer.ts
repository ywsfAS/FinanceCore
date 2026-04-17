export function useTransfer() {
    const transfer = async (payload: {
        accountId: string;
        toAccountId: string;
        amount: number;
        description: string;
        notes: string;
    }) => {
        const res = await fetch(`/api/v1/transactions/transfer`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Authorization: `Bearer ${localStorage.getItem("token")}`,
            },
            body: JSON.stringify(payload),
        });

        if (!res.ok) throw new Error("Transfer failed");

        return res.json();
    };

    return { transfer };
}