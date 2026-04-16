import styles from './OverviewHeader.module.css';

type OverviewHeaderProps = {
    title: string,
    description : string,
}

export default function OverviewHeader({title,description} : OverviewHeaderProps ) {
    return (
        <div className={styles.header}>
            <h3 className={styles.title}>{title}</h3>
            <span className={styles.desc}>{description}</span>
        </div>
    );
}