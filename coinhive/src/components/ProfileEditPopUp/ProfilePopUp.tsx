import Input from "../Input/Input";
import styles from "./ProfilePopUp.module.css";
import Button from "../Button/Button";
import { useState } from 'react';
import type { Profile } from "../../entities/profile"; 

interface Form {
    name: string;
    bio: string;
    photo: File | null;
}
const initialForm: Form = {
    name: "",
    bio: "",
    photo: null
};

interface ProfileEditPopUpProps {
    EditProfileHandler: (updatedData: Profile) => Promise<{ success: boolean; error?: any }>;
    PopUpHandler: () => void;
}

const ProfileEditPopUp = ({ EditProfileHandler , PopUpHandler }: ProfileEditPopUpProps) => {
    const [formData, setFormData] = useState<Form>(initialForm);
    const [errors, setErrors] = useState<Record<string, string>>({});
    const [isSaving, setIsSaving] = useState(false);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormData((prev) => ({ ...prev, [name]: value }));
        if (errors[name]) setErrors((prev) => ({ ...prev, [name]: "" }));
    };

    const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.target.files && e.target.files[0]) {
            setFormData((prev) => ({ ...prev, photo: e.target.files![0] }));
            if (errors.photo) setErrors((prev) => ({ ...prev, photo: "" }));
        }
    };

    const validateForm = (): boolean => {
        const newErrors: Record<string, string> = {};
        if (!formData.name.trim()) newErrors.name = "Name is required";
        if (!formData.bio.trim()) newErrors.bio = "Bio is required";
        if (!formData.photo) newErrors.photo = "Photo is required";

        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault(); 

        if (!validateForm()) return;

        setIsSaving(true);
        try {
            const nameParts = formData.name.trim().split(" ");
            const firstName = nameParts[0] || "";
            const lastName = nameParts.slice(1).join(" ") || "";

            const updatedProfilePayload: Profile = {
                firstName,
                lastName,
                bio: formData.bio,
                photo: formData.photo 
            };

            const result = await EditProfileHandler(updatedProfilePayload);

            if (result.success) {
                console.log("Profile successfully updated!");
            } else {
                console.log("Server error encountered while saving profile.");
            }
        } catch (err) {
            console.error("Submission crash:", err);
        } finally {
            setIsSaving(false);
        }
        PopUpHandler();
    };

    const handleReset = () => {
        setFormData(initialForm);
        setErrors({});
    };

    return (
        <div className={styles.overlay}>
            <form onSubmit={handleSubmit} className={styles.popUp}>
                <button
                    className={styles.closeButton}
                    onClick={PopUpHandler}
                    aria-label="Close popup"
                >
                    ×
                </button>
                <h1 className={styles.title}>Edit Your Profile</h1>

                <div className={styles.name}>
                    <Input value={formData.name} name="name" error={errors.name} onChange={handleChange} type="text" borderStyle="dashed" label="Name" placeholder="Enter your new name" />
                </div>
                <div className={styles.bio}>
                    <Input value={formData.bio} name="bio" error={errors.bio} onChange={handleChange} type="text" label="Bio" borderStyle='dashed' placeholder="Enter your new bio" />
                </div>
                <div className={styles.file}>
                    <Input error={errors.photo} onChange={handleFileChange} type="file" label={formData.photo ? `Selected: ${formData.photo.name}` : "Upload Photo"} id="photo" borderStyle='dashed' accept="image/*" />
                </div>

                <div className={styles.actions}>
                    <Button type="button" variant='purple' onClick={handleReset} disabled={isSaving}>
                        Reset
                    </Button>
                    <Button type="submit" variant='purple' disabled={isSaving}>
                        {isSaving ? "Saving..." : "Save"}
                    </Button>
                </div>
            </form>
        </div>
    );
};

export default ProfileEditPopUp;