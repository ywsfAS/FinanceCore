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

            <div className="profile-left-section">
                <Card>
                 
                    <div className="profile-user-section">
                        <div className="profile-user-image">
                            <img src={Logo} className="user-logo" />
                        </div>
                        <div className="profile-user-data">
                            <div className="user-header">
                                <div className="user-info">
                                    <h2 className="user-name">Youssef SK</h2>
                                    <span className="user-plan">Premium Account</span>
                                    <p className="user-bio">
                                        Focused on building long-term wealth and tracking every expense.
                                    </p>
                                    <UserTags/>
                                </div>

                                <div className="user-balance">
                                    <span className="balance-label">Total Balance</span>
                                    <h3 className="balance-amount">$12,450.00</h3>
                                    <span className="balance-change positive">+5.2% this month</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </Card>
                <Card>
                    <SpendingAnalytics/>
                </Card>
                <Card>
                    <FinancialCard/>
                </Card>

            </div>

            <div className="profile-right-section">
                <Card>
                    
                    <FinancialTransaction/>
                </Card>
                <Card>
                    <FinancialCard />
                </Card>
            </div>


        </div>
    )
}