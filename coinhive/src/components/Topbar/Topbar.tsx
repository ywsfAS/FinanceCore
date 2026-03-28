import styles from './Topbar.module.css';

type TopbarProps =  {
    username?: string;
    date: string;
}

export const Topbar = ({ username = 'Guest', date }: TopbarProps) => {
    return (
        <div className={styles.topbar}>
            <div className={styles.left}>
                <h1>Overview</h1>
                <p>{date}</p>
            </div>
            <div className={styles.avatar}>{username}</div>
        </div>
    );
};