import { BrowserRouter, Routes, Route } from "react-router-dom";
import ProtectedRoute from "./ProtectedRoute";

import RegisterPage from "../pages/Register/RegisterPage";
import LoginPage from "../pages/Login/LoginPage";

const AppRoutes = () => {
    return (
        <BrowserRouter>
            <Routes>
                // Public routes
                <Route path="/register" element={<RegisterPage />} />
                <Route path="/login" element={<LoginPage />} />
                // Protected routes
                <Route element={<ProtectedRoute />}>
                </Route>

            </Routes>
        </BrowserRouter>
    );
};

export default AppRoutes;