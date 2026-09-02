const BASE_URL = "https://localhost:7143/api/v1";
let refreshRequest: Promise<string | null> | null = null;

const refreshAccessToken = async (): Promise<string | null> => {
    const refreshToken = localStorage.getItem("refreshToken");
    if (!refreshToken) return null;
    if (refreshRequest) return refreshRequest;

    refreshRequest = fetch(`${BASE_URL}/auth/refresh`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ refreshToken }),
    })
        .then(async (response) => {
            if (!response.ok) return null;
            const data = await response.json() as { token?: string; refreshToken?: string };
            if (!data.token) return null;
            localStorage.setItem("token", data.token);
            if (data.refreshToken) localStorage.setItem("refreshToken", data.refreshToken);
            return data.token;
        })
        .catch(() => null)
        .finally(() => {
            refreshRequest = null;
        });

    return refreshRequest;
};

export class ApiError extends Error {
    status: number;
    data: unknown;

    constructor(message: string, status: number, data?: unknown) {
        super(message);
        this.status = status;
        this.data = data;
    }
}

export const apiClient = async <T>(
    endpoint: string,
    options?: RequestInit,
    canRetry = true
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

    if (res.status === 401 && canRetry && !endpoint.startsWith("/auth/")) {
        const newToken = await refreshAccessToken();
        if (newToken) return apiClient<T>(endpoint, options, false);
    }

    if (!res.ok) {
        const message = typeof data === "object" && data !== null && "message" in data && typeof data.message === "string"
            ? data.message
            : "API Request Failed";
        throw new ApiError(
            message,
            res.status,
            data
        );
    }

    return data;
};