const BASE_URL = "https://localhost:7143/api/v1";

export class ApiError extends Error {
    status: number;
    data: any;

    constructor(message: string, status: number, data?: any) {
        super(message);
        this.status = status;
        this.data = data;
    }
}

export const apiClient = async <T>(
    endpoint: string,
    options?: RequestInit
): Promise<T> => {
    const token = localStorage.getItem("token");
    const isFormData = options?.body instanceof FormData;

    const res = await fetch(`${BASE_URL}${endpoint}`, {
        ...options,
        headers: {
            ...(isFormData ? {} : { "Content-Type": "application/json" }),
            ...(token ? { Authorization: `Bearer ${token}` } : {}),
            ...(options?.headers || {}),
        },
    });

    const data = await res.json().catch(() => null);

    if (!res.ok) {
        throw new ApiError(
            data?.message || "API Request Failed",
            res.status,
            data
        );
    }

    return data;
};