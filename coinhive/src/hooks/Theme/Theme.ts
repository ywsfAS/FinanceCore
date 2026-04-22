import { useContext, useState } from "react"
import { ThemeContext } from "../../context/Theme/ThemeContext"

export const useTheme = () => {
    return useContext(ThemeContext);
} 
