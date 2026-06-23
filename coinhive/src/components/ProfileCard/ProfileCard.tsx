import { UserRoundPen } from "lucide-react";

import styles from "./ProfileCard.module.css";
import Image from "../../assets/pfp.jpeg";

import {
    NAV_ITEMS,
    DEFAULT_PROFILE,
} from "./constants";

export default function ProfileCard({
    profileData,
    PopUpHandler,
    TabHandler,
    active
}) {
    const data = profileData ?? DEFAULT_PROFILE;
    const name = `${data.firstName} ${data.lastName}`;

    return (
        <aside className={styles.card}>
            <div className={styles.avatar}>
                <img
                    src={Image}
                    alt={name}
                    className={styles.image}
                />

                <span className={styles.status} />
            </div>

            <h2 className={styles.name}>
                {name}
            </h2>

            <p className={styles.role}>
                {data.role}
            </p>

            <p className={styles.bio}>
                {data.bio}
            </p>

            <UserRoundPen
                size={20}
                className={styles.editIcon}
                onClick={PopUpHandler}
            />

            <div className={styles.divider} />


            <nav className={styles.nav}>
                {NAV_ITEMS.map((item, index) => (
                    <div
                        key={item.name}
                        className={`${styles.navItem} ${active === index
                                ? styles.navActive
                                : ""
                            }`}
                        onClick={() => TabHandler(index)}
                    >
                        {item.icon}
                        {item.name}
                    </div>
                ))}
            </nav>
        </aside>
    );
}