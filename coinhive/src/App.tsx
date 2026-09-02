import { Route, Routes } from "react-router-dom";
import Navbar from './components/Navbar/Navbar';
import RegisterPage from './pages/Register/RegisterPage';
import ProfilePage from './pages/Profile/Profile';
import About from './pages/About/About';
import Contact from './pages/Contact/Contact'
import Footer from './components/Footer/Footer';
import Landing from './pages/Landing/Landing';
import LoginPage from './pages/Login/LoginPage';
import ForgotPasswordPage from "./pages/ForgotPasswordPage/Forgotpassword";
import ResetPasswordPage from "./pages/ResetPasswordPage/Resetpasswordpage";
import BudgetsPage from "./pages/Budgets/BudgetsPage"

function App() {
    return (
        <>
            <Navbar />
            <Routes>
                <Route path="/" element={<Landing />} />
                <Route path="/about" element={<About />} />
                <Route path="/contact" element={<Contact />} />
                <Route path="/register" element={<RegisterPage />} />
                <Route path="/login" element={<LoginPage />} />
                <Route path="/forgot-password" element={<ForgotPasswordPage />} />
                <Route path="/forget-password" element={<ForgotPasswordPage />} />
                <Route path="/reset-password" element={<ResetPasswordPage />} />
                <Route path="/profile" element={<ProfilePage />} />
                <Route path="/budgets" element={<BudgetsPage />} />

            </Routes>
            <Footer />
        </>
    )
}

export default App
