import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../hooks/Auth";


const ProtectedRoute = () => {
    const { isAuthenticated, loading } = useAuth();
    // loading until authenticated
    if (loading) return <div>Loading...</div>;
    if (!isAuthenticated) return <Navigate to="/login" replace />;
    return <Outlet/>;
}
export default ProtectedRoute; 
