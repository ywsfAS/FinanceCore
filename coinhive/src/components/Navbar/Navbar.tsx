import "./Navbar.css";
import { Link } from "react-router-dom";
import { useAuth } from "../../hooks/Auth";
import Logo from "../../assets/logo.svg";
const Navbar = () => {
    const { user, isAuthenticated, logout } = useAuth();

    return (
        <nav className="navbar">
            <div className="navbar-left nav-logo-container">
                <img className="nav-logo" src={Logo} alt="Logo" />
            </div>
            <div className="nav-center">
                {(isAuthenticated) && (
                    <div className="nav-links">
                        <Link to="/dashboard">Dashboard</Link>
                        <Link to="/transactions">Transactions</Link>
                        <Link to="/accounts">Accounts</Link>
                        <Link to="/profile">profile</Link>
                    </div>
            )}

            </div>


            <div className="navbar-right">
                {!isAuthenticated ? (
                    <>
                        <Link to="/login" className="nav-btn">
                            Login
                        </Link>
                        <Link to="/register" className="nav-btn primary">
                            Register
                        </Link>
                    </>
                ) : (
                    <div className="user-section">
                        <span className="user-name">
                            {user?.name || "User"}
                        </span>

                        <Link to="/profile" className="nav-btn">
                            Profile
                        </Link>

                        <button onClick={logout} className="nav-btn logout">
                            Logout
                        </button>
                    </div>
                )}
            </div>
        </nav>
    );
};

export default Navbar;