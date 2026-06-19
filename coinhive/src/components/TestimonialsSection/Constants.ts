

export interface Testimonial {
    quote: string;
    name: string;
    role: string;
    initials: string;
    color: string;
    stars: number;
}

export const testimonials: Testimonial[] = [
    {
        quote: "FinanceCore replaced four apps I was using. My net worth went up 18% the first year just because I could actually see where my money was going.",
        name: "Layla Hassan",
        role: "Freelance Designer, Dubai",
        initials: "LH",
        color: "#10b981",
        stars: 5,
    },
    {
        quote: "The AI insights are genuinely useful it caught a subscription I forgot about and flagged that I was overspending on dining before I even noticed.",
        name: "Tom Eriksson",
        role: "Software Engineer, Stockholm",
        initials: "TE",
        color: "#3b82f6",
        stars: 5,
    },
    {
        quote: "I've tried Mint, YNAB, and Personal Capital. FinVault is the first one that didn't feel like homework. The UX is just clean and it works.",
        name: "Priya Nair",
        role: "Product Manager, London",
        initials: "PN",
        color: "#8b5cf6",
        stars: 5,
    },
    {
        quote: "I hit my emergency fund goal in 11 months using the goal tracker. Having a visual progress bar genuinely changed my savings behavior.",
        name: "Amara Diallo",
        role: "Nurse, Paris",
        initials: "AD",
        color: "#ef4444",
        stars: 5,
    },
    {
        quote: "Our startup uses FinVault for expense tracking across the team. Saved us from hiring a bookkeeper for the first two years.",
        name: "James Wu",
        role: "Co-Founder, Singapore",
        initials: "JW",
        color: "#06b6d4",
        stars: 5,
    },
];

