import { useState , useEffect } from "react"
import { ThemeContext } from "./ThemeContext";
type ThemeProiderProps = {
    children : ReactNode
}
export const ThemeProvider = ({children} : ThemeProiderProps) => {
    const [theme, setTheme] = useState<'light' | 'dark'>(() => {
        return (localStorage.getItem("theme") as "light" | "dark") || "light";
    });

    useEffect(() => {
        localStorage.setItem("theme", theme);
    }, [theme])
    const toggleTheme = () => {
        setTheme((prev) => prev === "light" ? "dark" : "light");
    }
    return (
        <ThemeContext.Provider
        value = {{
        theme: theme,
        toggleTheme : toggleTheme
        }}
        >
        {children}
        </ThemeContext.Provider>
        
    )
}