import styles from './ProfileCard.module.css';
import Image from "../../assets/profle.jpeg";

const NAV_ITEMS = ['Overview', 'Transactions', 'Investments', 'Budgets', 'Reports'];
type profileProps = {
    profileData : profileData | null,
}
interface profileData {
    firstName: string,
    lastName: string,
    role: string,
    srcUrl?: string,
    bio : string
}
const defaultProfile : profileData = {
    firstName: 'Jordan',
    lastName: 'Mitchell',
    role: 'ADMINISTRATOR · PREMIUM',
    bio: 'Financial analyst &amp; personal finance enthusiast. Tracking goals since 2021. Building toward early financial independence.',

}
export default function ProfileCard({ profileData }: profileProps) {
    const data = profileData ?? defaultProfile;
    const name = data.firstName + ' ' + data.lastName;
    return (
        <aside className={styles.card}>
            <div className={styles.avatar}>
                    <img
                        src={Image}
                         alt={name }
                        className={styles.image}
                    />
                <span className={styles.status} />
            </div>

            <h2 className={styles.name}>{name}</h2>
            <p className={styles.role}>{defaultProfile.role}</p>
            <p className={styles.bio}>
                {data.bio }
            </p>

            <div className={styles.divider} />

            <div className={styles.statsBox}>
                {[
                    { value: '$142k', label: 'Net Worth' },
                    { value: '30%', label: 'Saved' },
                    { value: '97', label: 'Score' },
                ].map(({ value, label }) => (
                    <div key={label} className={styles.statItem}>
                        <span className={styles.statValue}>{value}</span>
                        <span className={styles.statLabel}>{label}</span>
                    </div>
                ))}
            </div>

            <nav className={styles.nav}>
                {NAV_ITEMS.map((item, i) => (
                    <div key={item} className={`${styles.navItem} ${i === 0 ? styles.navActive : ''}`}>
                        <span className={styles.navDot} />
                        {item}
                    </div>
                ))}
            </nav>
        </aside>
    );
}