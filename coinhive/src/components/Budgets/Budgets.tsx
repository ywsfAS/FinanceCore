
import { useState } from 'react';
import BudgetCard from '../BudgetCard/BudgetCard';
import BudgetCreatePopup from '../BudgetCreatePopup/BudgetCreatePopup';
import SectionHeader from '../SectionHeader/SectionHeader';
import styles from './Budgets.module.css';
import { HEADER, MOCK_BUDGETS } from './constants';

const Budgets = () => {
    const [open, setOpen] = useState(false);

    const handleClose = () => {
        setOpen((prev) => !prev);
    };

    return (
        <div className={styles.wrapper}>
            <SectionHeader title={HEADER.title} subtitle={HEADER.subtitle} btnName={HEADER.btnName} handler={handleClose} />

            <div className={styles.cards}>
                {MOCK_BUDGETS.map((budget) => (
                    <BudgetCard
                        key={budget.id}
                        id={budget.id}
                        name={budget.name}
                        amount={budget.amount}
                        currency={budget.currency}
                        categoryName={budget.categoryName}
                        period={budget.period}
                        startDate={budget.startDate}
                        endDate={budget.endDate}
                    />
                ))}
            </div>

            {open && <BudgetCreatePopup handleClose={handleClose} />}
        </div>
    );
};

export default Budgets;