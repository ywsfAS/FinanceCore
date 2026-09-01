

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
        quote: "Before FinanceCore, I checked three different apps just to understand my finances. Now everything is in one place, and for the first time I actually feel in control of my money.",
        name: "Layla Hassan",
        role: "Freelance Designer, Dubai",
        initials: "LH",
        color: "#10b981",
        stars: 5,
    },
    {
        quote: "The spending insights have saved me hundreds. FinanceCore spotted recurring charges I completely forgot about and helped me build better habits without feeling restrictive.",
        name: "Tom Eriksson",
        role: "Software Engineer, Stockholm",
        initials: "TE",
        color: "#3b82f6",
        stars: 5,
    },
    {
        quote: "Most finance apps overwhelm you with charts. FinanceCore gives me exactly what I need to know at a glance. Clean, fast, and surprisingly enjoyable to use.",
        name: "Priya Nair",
        role: "Product Manager, London",
        initials: "PN",
        color: "#8b5cf6",
        stars: 5,
    },
    {
        quote: "The goal tracker completely changed how I save. Watching my progress grow each month kept me motivated, and I reached my emergency fund months earlier than planned.",
        name: "Amara Diallo",
        role: "Nurse, Paris",
        initials: "AD",
        color: "#ef4444",
        stars: 5,
    },
    {
        quote: "As a founder, I need visibility into every dollar. FinanceCore gives me a clear picture of our finances without the complexity of traditional accounting software.",
        name: "James Wu",
        role: "Co-Founder, Singapore",
        initials: "JW",
        color: "#06b6d4",
        stars: 5,
    },
];

export const animationConfig = (index: number) => (
    {

        initial: {
            opacity: 0,
        },

        whileInView: {
            opacity: 1,
            x: 0,
            y: 0,
        },

        whileHover: {
            scale: 1.04,
            transition: {
                type: "spring",
                stiffness: 400,
                damping: 25,
            },
        },

        viewport: {
            once: true,
            amount: 0.3,
        },

        transition: {
            duration: 0.3,
            delay: index * 0.1,
        },
    }
);