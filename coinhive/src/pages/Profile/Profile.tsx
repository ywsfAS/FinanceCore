import Card from "../../components/Card/Card";
import SideImage from "../../components/SideImage/SideImage";
import Logo from "../../assets/profile.jpeg";
import UserTags from "../../components/TagButtons/TagButton";
import "./Profile.css";
export default function ProfilePage() {

    return (
        <div className="profile-container">

            <div className="profile-left-section">
                <Card>
                    <div className="profile-user-section">
                        <div className="profile-user-image">
                            <SideImage src={Logo} />
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
                    <h2>Spending Analytics</h2>
                </Card>
                <Card>
                    <h2>Financial Details</h2>
                </Card>

            </div>

            <div className="profile-right-section">
                <Card>
                    <h2>Recent Transactions</h2>
                </Card>
                <Card>
                    <h2>Financial Goals</h2>
                </Card>
            </div>


        </div>
    )
}