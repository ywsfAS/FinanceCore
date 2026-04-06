import styles from "./SideImage.module.css";

interface SideImageProps {
    src: string;
    alt?: string;
    className?: string;
}

export default function SideImage({ src, alt = "", className = "" }: SideImageProps) {
    return <div className={`${styles.sideImage} ${className}`}>
        <img src={src} alt={alt} />
    </div>;
}