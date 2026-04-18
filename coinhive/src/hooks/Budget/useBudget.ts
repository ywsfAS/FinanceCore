
import { useState, useEffect } from "react";
import { request } from "../helper";
const BASE_URL = "/api/v1/budgets";

export function useCreateBudget() {
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    const createBudget = async (data) => {
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

    return { createBudget, loading, error };
}

export function useBudget(id) {
    const [data, setData] = useState(null);
    const [loading, setLoading] = useState(!!id);
    const [error, setError] = useState(null);

    useEffect(() => {
        if (!id) return;

        const fetchBudget = async () => {
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

        fetchBudget();
    }, [id]);

    return { data, loading, error };
}

export function useUpdateBudget() {
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    const updateBudget = async (id, data) => {
        setLoading(true);
        setError(null);

        try {
            await request(`${BASE_URL}/${id}`, {
                method: "PUT",
                body: JSON.stringify(data),
            });
        } catch (err) {
            setError(err);
        } finally {
            setLoading(false);
        }
    };

    return { updateBudget, loading, error };
}

export function useDeleteBudget() {
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    const deleteBudget = async (id) => {
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

    return { deleteBudget, loading, error };
}

export function useBudgetProgress(id) {
    const [progress, setProgress] = useState(null);
    const [loading, setLoading] = useState(!!id);
    const [error, setError] = useState(null);

    useEffect(() => {
        if (!id) return;

        const fetchProgress = async () => {
            setLoading(true);
            setError(null);

            try {
                const result = await request(`${BASE_URL}/${id}/progress`);
                setProgress(result);
            } catch (err) {
                setError(err);
            } finally {
                setLoading(false);
            }
        };

        fetchProgress();
    }, [id]);

    return { progress, loading, error };
}