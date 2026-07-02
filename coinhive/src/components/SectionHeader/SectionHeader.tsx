import styles from './SectionHeader.module.css';
import type { SectionHeaderProps } from './types';
import Button from '../Button/Button';

const SectionHeader = ({ title, subtitle, btnName, handler }: SectionHeaderProps) => {
    return (
        <div className={styles.header}>
            <div>
                <h1 className={styles.title}>{title}</h1>
                <p className={styles.subtitle}>
                    {subtitle}
                </p>
            </div>

            <Button
                onClick={handler}
            >
                {btnName}
            </Button>
        </div>
    )
}
export default SectionHeader;