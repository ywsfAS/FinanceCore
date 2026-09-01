import { EnCurrency } from '../../entities/Currency';
import type { AccountSummary, UserSummary } from '../../entities/Report';
import { useUserSummary } from '../../hooks/Reports/useUserSummary';
import styles from './ProfileStats.module.css';
import { stats } from './constants';

export default function StatsGrid() {
    const { isLoading, data, error, isError } = useUserSummary();
    if (isLoading) return <div>loading...</div>;
    if (isError) return <div>{error.message}</div>;
    const userSummary: UserSummary = (data ?? []).reduce(
        (acc: UserSummary, account: AccountSummary) => ({
            totalIncome: acc.totalIncome + account.totalIncome,
            totalExpense: acc.totalExpense + account.totalExpense,
            netSavings: acc.netSavings + acc.netSavings,
            currency: acc.currency
        }),
        {
            totalIncome: 0,
            totalExpense: 0,
            netSavings: 0,
            currency: EnCurrency.USD
        }

    );
    const currency = userSummary.currency;
    const summary = [
        {
            ...stats.income,
            value: userSummary.totalIncome,
        },
        {
            ...stats.expense,
            value: userSummary.totalExpense,
        },
        {
            ...stats.savings,
            value: userSummary.netSavings,
        },
    ];

    return (
        <div className={styles.grid}>
            {summary.map((s) => {
                const Icon = s.icon;
                return (
                    <div key={s.label} className={styles.box}>
                        <div className={styles.title}>
                            <div className={styles.icon}>
                                <Icon />
                            </div>
                            <div className={styles.label}>{s.label}</div>
                        </div>
                        <div className={styles.value}>{s.value} {currency}</div>
                    </div>)
            })}
        </div>
    );
}