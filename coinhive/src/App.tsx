import { useAuth } from "./hooks/Auth";
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
               
                <Route path="/profile" element={<ProfilePage/> }/>

            </Routes>
            <Footer/>
        </>
    )
}

export default App
