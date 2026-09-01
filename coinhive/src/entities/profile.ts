
export interface Profile {
    userId?: string;
    firstName: string;
    lastName?: string;
    bio?: string;
    role?: string;
    avatarUrl?: string | null;
    photo?: File | string | null;
    currency?: string;
}