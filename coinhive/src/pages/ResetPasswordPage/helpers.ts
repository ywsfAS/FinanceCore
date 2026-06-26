
export type Strength = { score: 0 | 1 | 2 | 3 | 4; label: string; color: string; width: string };
export function getStrength(pw: string): Strength {
    if (!pw) return { score: 0, label: "", color: "#e5e7eb", width: "0%" };
    let score = 0;
    if (pw.length >= 8) score++;
    if (/[A-Z]/.test(pw)) score++;
    if (/[0-9]/.test(pw)) score++;
    if (/[^A-Za-z0-9]/.test(pw)) score++;

    const map: Record<number, Omit<Strength, "score">> = {
        1: { label: "Weak", color: "#ef4444", width: "25%" },
        2: { label: "Fair", color: "#f59e0b", width: "50%" },
        3: { label: "Good", color: "#3b82f6", width: "75%" },
        4: { label: "Strong", color: "#10b981", width: "100%" },
    };
    return { score: score as Strength["score"], ...(map[score] ?? map[1]) };
}
