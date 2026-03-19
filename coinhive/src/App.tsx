import './App.css'
import RegisterPage  from './pages/Register/RegisterPage'
import { useAuth } from "./hooks/Auth";
import { useEffect } from 'react';
import Navbar from './components/Navbar/Navbar';
import ProfilePage from './pages/Profile/Profile';

function App() {
    const { loginWithToken , loading } = useAuth();
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


    if(!loading) return <div>Loading...</div>
  return (
      <>
            <Navbar/>
            <ProfilePage/>
      </>
  )
}

export default App
