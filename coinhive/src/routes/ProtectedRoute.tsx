import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../hooks/Auth/Auth";


const ProtectedRoute = () => {
    const { isAuthenticated, loading } = useAuth();
    // loading until authenticated
    if(loading) return <div>loading...</div>
    if (!isAuthenticated) return <Navigate to="/login" replace />;
    return <Outlet/>;
}
export default ProtectedRoute; 
