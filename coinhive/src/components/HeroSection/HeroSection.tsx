import React from "react";
import styles from "./HeroSection.module.css";
import Button from "../Button/Button";
import Phone from "../../assets/mobile.png";
interface HeroSectionProps {
    title: string,
    description: string,
    note?: string,
    mainBtnText: string,
    secondBtnText : string,
}
const HeroSection: React.FC<HeroSectionProps> = ({title , description , mainBtnText , secondBtnText }) => {

    return (
        <section className={styles.hero} >
            <div className={styles.content}>
                <h1 className={styles.title}>{title}</h1>
                <p className={styles.description}>{description}</p>
                <div className={styles.btnContainer}>
                    <Button size='large'>{mainBtnText}</Button>
                    <Button variant='secondary' size='large'>{secondBtnText}</Button>
                </div>
            </div>
            <div className={styles.imageContainer}>
                <img src={Phone} />
            </div>
        </section>
    );
};

export default HeroSection;
