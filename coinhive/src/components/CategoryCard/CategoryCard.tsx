import styles from './CategoryCard.module.css';

interface CategoryCardProps {
    id: string;
    name: string;
    type: string;
    onEdit?: (id: string) => void;
    onDelete?: (id: string) => void;
}

const CategoryCard = ({
    id,
    name,
    type,
    onEdit,
    onDelete
}: CategoryCardProps) => {
    return (
        <div className={styles.card}>
            <div className={styles.left}>
                <h3>{name}</h3>

                <span
                    className={
                        type === 'Income'
                            ? styles.income
                            : styles.expense
                    }
                >
                    {type}
                </span>
            </div>

            <div className={styles.actions}>
                <button
                    className={styles.editBtn}
                    onClick={() => onEdit?.(id)}
                >
                    Edit
                </button>

                <button
                    className={styles.deleteBtn}
                    onClick={() => onDelete?.(id)}
                >
                    Delete
                </button>
            </div>
        </div>
    );
};

export default CategoryCard;