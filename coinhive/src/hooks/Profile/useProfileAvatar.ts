import { useMemo } from "react";
import defaultProfileImage from "../../assets/pfp.jpeg";

const STATIC_PROFILE_BASE_URL = "https://localhost:7143/uploads/users/profiles";

export function useProfileAvatar(avatarUrl?: string | null) {
    return useMemo(() => {
        if (!avatarUrl || avatarUrl.trim() === "") {
            return defaultProfileImage;
        }

        const normalized = avatarUrl.trim();

        if (/^https?:\/\//i.test(normalized)) {
            return normalized;
        }

        if (normalized.startsWith("/uploads/")) {
            return `https://localhost:7143${normalized}`;
        }

        return `${STATIC_PROFILE_BASE_URL}/${normalized}`;
    }, [avatarUrl]);
}
