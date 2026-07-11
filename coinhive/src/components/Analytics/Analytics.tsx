import { BarChartCard } from "../BarChartCard/BarChartCard";
import { PieChartCard } from "../PieChartCard/PieChartCard";
import ProgressRingCard from "../ProgressRingCard/ProgressRingCard";
import SectionHeader from "../SectionHeader/SectionHeader";
import SummaryCard from "./SummaryCard";
import styles from "./Analytics.module.css";
import { HEADER, PROGRESS_METRICS, SUMMARY_CARDS } from "./constant";

const Analytics = () => {
    const handleRefresh = () => {
        console.log("Refresh analytics view");
    };

    return (
        <div className={styles.wrapper}>
            <SectionHeader
                title={HEADER.title}
                subtitle={HEADER.subtitle}
                btnName={HEADER.btnName}
                handler={handleRefresh}
            />

            <div className={styles.summaryGrid}>
                {SUMMARY_CARDS.map((stat) => (
                    <SummaryCard key={stat.id} {...stat} />
                ))}
            </div>

            <div className={styles.mainGrid}>
                <div className={styles.chartAndProgressWrapper}>
                    <section className={styles.chartPanel}>
                        <div className={styles.chartHeader}>
                            <h2 className={styles.chartTitle}>Cashflow trends</h2>
                            <p className={styles.chartSubtitle}>
                                Compare income, expense, and category performance across the last months.
                            </p>
                        </div>
                        <div className={styles.barChartWrapper}>
                            <BarChartCard />
                        </div>
                    </section>

                    <div className={styles.progressGrid}>
                        {PROGRESS_METRICS.map((metric) => (
                            <ProgressRingCard
                                key={metric.title}
                                icon={metric.icon}
                                title={metric.title}
                                subtitle={metric.subtitle}
                                maxValue={metric.maxValue}
                                value={metric.value}
                                label={metric.label}
                                radius={metric.radius}
                            />
                        ))}
                    </div>
                </div>

                <aside className={styles.sideSection}>
                    <PieChartCard
                        title="Categories"
                        subtitle="A quick view of how your money is allocated across categories."
                    />
                </aside>
            </div>
        </div>
    );
};

export default Analytics;