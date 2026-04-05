import './App.css'
import { useAuth } from "./hooks/Auth";
import { useEffect } from 'react';
import Navbar from './components/Navbar/Navbar';
import RegisterPage from './pages/Register/RegisterPage';
import ProfilePage from './pages/Profile/Profile';
import About from './pages/About/About';
import Contact from './pages/Contact/Contact'
import Footer from './components/Footer/Footer';
import Landing from './pages/Landing/Landing';

function App() {
    const { loginWithToken, loading } = useAuth();
    // On Mount
    useEffect(() => {
        const runAuth = async () => {
            const token: string | null = localStorage.getItem("token");
            if (token) {
                await loginWithToken(token);
            }
        }
        runAuth();
    }, []);


    if (!loading) return <div>Loading...</div>
    return (
        <>
            <Navbar />
            <Landing />
            <Footer/>
        </>
    )
}

export default App
