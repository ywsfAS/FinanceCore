import "./FinancialDetails.css";

export default function FinancialDetails() {
    return (
        <div className="financial-details">
            <h3>Financial Details</h3>

            <div className="details-grid">
                <div className="detail-item">
                    <span>Currency</span>
                    <strong>USD ($)</strong>
                </div>

                <div className="detail-item">
                    <span>Country</span>
                    <strong>Morocco</strong>
                </div>

                <div className="detail-item">
                    <span>Linked Banks</span>
                    <strong>2 Accounts</strong>
                </div>

                <div className="detail-item">
                    <span>Plan</span>
                    <strong>Premium</strong>
                </div>

                <div className="detail-item">
                    <span>Risk Profile</span>
                    <strong>Moderate</strong>
                </div>
            </div>
        </div>
    );
}