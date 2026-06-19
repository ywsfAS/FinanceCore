import React from "react";
import styles from "./LogoSection.module.css";
import Svg from "../../assets/brand.svg";

const logos = new Array(5).fill(Svg);

const LogoSection: React.FC = () => {
    return (
        <section className={styles.wrapper} >
            <h2 className={styles.title}>Partners We Trust</h2>
            <p className={styles.label}>Partnering with industry-leading companies to help you manage your finances with confidence.</p>
            <div className={styles.logos}>
                {logos.map((name , i) => (
                    <img key={i} className={styles.logo} src={name} />
                ))}
            </div>
        </section>
    );
};

export default LogoSection;