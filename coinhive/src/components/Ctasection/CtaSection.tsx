import React from "react";
import styles from "./CtaSection.module.css";
import Button from "../Button/Button";
interface CtaSectionProps {
    title: string,
    para: string,
    mainBtnMsg: string,
    secondBtnMsg: string,
}


const CtaSection: React.FC<CtaSectionProps> = ({title , para , mainBtnMsg , secondBtnMsg}) => {

    return (
        <section className={styles.wrapper} >
            <div className={styles.card}>
                    <h2 className={styles.title}>
                        {title}<br />Starts Right Now
                    </h2>
                    <p className={styles.subtitle}>
                        {para}
                    </p>
                    <div className={styles.actions}>
                        <Button>{mainBtnMsg}</Button>
                        <Button variant="secondary">{secondBtnMsg}</Button>
                    </div>
                </div>
        </section>
    );
};

export default CtaSection;