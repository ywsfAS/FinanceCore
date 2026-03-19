import { useState } from "react";
import "./TagButton.css";
const tagsList = ["Investing", "Saving", "Budgeting", "Crypto"];

export default function UserTags() {
    const [activeTags, setActiveTags] = useState<string[]>([]);

    const toggleTag = (tag: string) => {
        setActiveTags((prev) =>
            prev.includes(tag)
                ? prev.filter((t) => t !== tag)
                : [...prev, tag]
        );
    };

    return (
        <div className="user-tags">
            {tagsList.map((tag) => (
                <span
                    key={tag}
                    className={`tag ${activeTags.includes(tag) ? "active" : ""}`}
                    onClick={() => toggleTag(tag)}
                >
                    {tag}
                </span>
            ))}
        </div>
    );
}