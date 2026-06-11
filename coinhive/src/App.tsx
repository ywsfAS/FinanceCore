import { useAuth } from "./hooks/Auth/Auth";
import { useEffect } from 'react';
import { Route, Routes } from "react-router-dom";
import Navbar from './components/Navbar/Navbar';
import RegisterPage from './pages/Register/RegisterPage';
import ProfilePage from './pages/Profile/Profile';
import About from './pages/About/About';
import Contact from './pages/Contact/Contact'
import Footer from './components/Footer/Footer';
import Landing from './pages/Landing/Landing';
import LoginPage from './pages/Login/LoginPage';
import ProtectedRoute from './routes/ProtectedRoute';
import ForgotPasswordPage from "./pages/ForgotPasswordPage/ForgotpasswordpagE";
import ResetPasswordPage from "./pages/ResetPasswordPage/Resetpasswordpage";
import TransactionsPage from "./pages/Transactions/transactionsPage";
import AccountsPage from "./pages/Accounts/AccountsPage";
import CategoriesPage from "./pages/Categories/CategoriesPage";
import BudgetsPage from "./pages/Budgets/BudgetsPage"

function App() {
    const { user, loginWithToken} = useAuth();
    // On Mount
    useEffect(() => {
        if(user?.token) loginWithToken(user.token);
    }, []);
    return (
        <>
            <Navbar />
            <Routes>
                <Route path="/" element={<Landing />} />
                <Route path="/about" element={<About />} />
                <Route path="/contact" element={<Contact />} />
                <Route path="/register" element={<RegisterPage />} />
                <Route path="/login" element={<LoginPage />} />
                <Route path="/forget-password" element={<ForgotPasswordPage/> } />
                <Route path="/reset-password" element={<ResetPasswordPage/> } />
                <Route path="/profile" element={<ProfilePage />} />
                <Route path="/transactions" element={<TransactionsPage />} />
                <Route path="/accounts" element={<AccountsPage />} />
                <Route path="/categories" element={<CategoriesPage/> } />
                <Route path="/budgets" element={<BudgetsPage/>}/>

            </Routes>
            <Footer/>
        </>
    )
}

export default App
