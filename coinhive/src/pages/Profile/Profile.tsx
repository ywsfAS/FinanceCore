import Card from "../../components/Card/Card";
import SideImage from "../../components/SideImage/SideImage";
import Logo from "../../assets/profile.jpeg";
import UserTags from "../../components/TagButtons/TagButton";
import "./Profile.css";
import SpendingAnalytics from "../../components/SpendingAnalytics/SpendingAnalytics";
import FinancialCard from "../../components/FinancialOverview/FinancialOverview";
import FinancialTransaction from "../../components/FinancialTransaction/FinancialTransaction";
export default function ProfilePage() {

    return (
        <div className="profile-container">
            <SpendingAnalytics/>
        </div>
    )
}