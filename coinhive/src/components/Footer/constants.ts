import type { FooterItem, FooterProps } from "./types";


const ITEMS: FooterItem[] = [
    {
        groupName: "Product",
        groupOptions: ["Dashboard", "Goals", "Analytics", "Integrations"]
    },
    {
        groupName: "Company",
        groupOptions: ["About Us", "Careers", "Press", "Blog"]
    },

    {
        groupName: "Legal",
        groupOptions: ["Privacy Policy", "Terms of Service", "Security"]
    },
]
export const DEFAULT: FooterProps = {
    logo: "FinanceCore",
    tagline: "Manage your finances with confidence.",
    items: ITEMS,
    copyright: " Finance Core. All rights reserved."
}