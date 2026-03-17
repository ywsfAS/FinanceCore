import "./SideImage.css";

interface SideImageProps {
    src: string;
    alt?: string;
    className?: string;
}

export default function SideImage({ src, alt = "", className = "" }: SideImageProps) {
    return <div className={`side-image ${className}`}>
        <img src={src} alt={alt} />
    </div>;
}