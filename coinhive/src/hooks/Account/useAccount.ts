
import { useState, useEffect } from "react";

const BASE_URL = "/api/v1/accounts";

async function request(url, options = {}) {
    const res = await fetch(url, {
        headers: {
            "Content-Type": "application/json",
        },
        ...options,
    });

    if (!res.ok) {
        throw new Error("Request failed");
    }

    return res.json();
}

export function useCreateAccount() {
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    const createAccount = async (data) => {
        setLoading(true);
        setError(null);

        try {
            const result = await request(BASE_URL, {
                method: "POST",
                body: JSON.stringify(data),
            });
            return result;
        } catch (err) {
            setError(err);
        } finally {
            setLoading(false);
        }
    };

    return { createAccount, loading, error };
}

export function useUpdateAccount() {
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    const updateAccount = async (data) => {
        setLoading(true);
        setError(null);

        try {
            const result = await request(BASE_URL, {
                method: "PUT",
                body: JSON.stringify(data),
            });
            return result;
        } catch (err) {
            setError(err);
        } finally {
            setLoading(false);
        }
    };

    return { updateAccount, loading, error };
}

export function useAccount(id) {
    const [data, setData] = useState(null);
    const [loading, setLoading] = useState(!!id);
    const [error, setError] = useState(null);

    useEffect(() => {
        if (!id) return;

        const fetchAccount = async () => {
            setLoading(true);
            setError(null);

            try {
                const result = await request(`${BASE_URL}/${id}`);
                setData(result);
            } catch (err) {
                setError(err);
            } finally {
                setLoading(false);
            }
        };

        fetchAccount();
    }, [id]);

    return { data, loading, error };
}

export function useBalance() {
    const [balance, setBalance] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchBalance = async () => {
            try {
                const result = await request(`${BASE_URL}/balance`);
                setBalance(result);
            } catch (err) {
                setError(err);
            } finally {
                setLoading(false);
            }
        };

        fetchBalance();
    }, []);

    return { balance, loading, error };
}

export function useDeleteAccount() {
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    const deleteAccount = async (id) => {
        setLoading(true);
        setError(null);

        try {
            await request(`${BASE_URL}/${id}`, {
                method: "DELETE",
            });
        } catch (err) {
            setError(err);
        } finally {
            setLoading(false);
        }
    };

    return { deleteAccount, loading, error };
}

export function useAccountTransactions(accountId) {
    const [transactions, setTransactions] = useState([]);
    const [loading, setLoading] = useState(!!accountId);
    const [error, setError] = useState(null);

    useEffect(() => {
        if (!accountId) return;

        const fetchTransactions = async () => {
            setLoading(true);
            setError(null);

            try {
                const result = await request(
                    `${BASE_URL}/${accountId}/transactions`
                );
                setTransactions(result);
            } catch (err) {
                setError(err);
            } finally {
                setLoading(false);
            }
        };

        fetchTransactions();
    }, [accountId]);

    return { transactions, loading, error };
}