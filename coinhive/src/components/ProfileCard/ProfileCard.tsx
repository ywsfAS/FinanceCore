import styles from './ProfileCard.module.css';
import Image from "../../assets/pfp.jpeg";
import { PanelsTopLeft, BadgeDollarSign, Wallet, ClipboardMinus, Bitcoin } from "lucide-react";
import { useState } from 'react';
import { UserRoundPen } from "lucide-react";

const NAV_ITEMS = [{ name: 'Overview', icon: <PanelsTopLeft size={20} /> }, { name: 'Transactions', icon: <BadgeDollarSign  size={20}/> },
    { name: 'Investments', icon: < Bitcoin size={20}/> }, { name: 'Budgets', icon: <Wallet size={20}/> }, { name: 'Reports', icon: <ClipboardMinus size={20}/> }];
const defaultProfile = {
    firstName: 'Jordan',
    lastName: 'Mitchell',
    role: 'Software Engineer',
    photo: "photo",
    currency: "USD",
    bio: 'Financial analyst &amp; personal finance enthusiast. Tracking goals since 2021. Building toward early financial independence.',

}
export default function ProfileCard({ profileData , PopUpHandler }) {
    const data = profileData ?? defaultProfile;
    console.log("profile", data);
    const name = data.firstName + ' ' + data.lastName;
    const [active, setActive] = useState(0);
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
                {data.bio}
            </p>
            <UserRoundPen size={20} className={styles.editIcon} onClick={PopUpHandler} />
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
                    <div key={item.name} className={`${styles.navItem} ${i === active ? styles.navActive : ''}`} onClick={() => {
                    setActive(i)} }>
                        {item.icon }
                        {item.name}
                    </div>
                ))}
            </nav>
        </aside>
    );
}