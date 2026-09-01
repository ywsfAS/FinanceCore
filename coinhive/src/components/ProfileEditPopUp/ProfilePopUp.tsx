import { useEffect, useRef, useState } from "react";
import Input from "../Input/Input";
import Button from "../Button/Button";
import styles from "./ProfilePopUp.module.css";

import { useForm } from "react-hook-form";

import type {
    UpdateProfileParams,
    UploadProfileImageParams,
} from "../../services/profileService";

import {
    PROFILE_POPUP_TITLE,
    PROFILE_CLOSE_ARIA_LABEL,
    PROFILE_PHOTO_ACCEPT,
    PROFILE_FORM_LABELS,
    PROFILE_FORM_PLACEHOLDERS,
    PROFILE_FORM_BUTTONS,
} from "./constants";
import defaultProfileImage from "../../assets/pfp.jpeg";
import { useProfileAvatar } from "../../hooks/Profile/useProfileAvatar";

interface ProfileEditPopUpProps {
    EditProfileHandler: (
        profile: UpdateProfileParams
    ) => Promise<void>;

    EditProfileImageHandler: (
        image: UploadProfileImageParams
    ) => Promise<void>;

    PopUpHandler: () => void;
    avatarUrl?: string | null;
}

interface ProfileForm {
    firstName: string;
    lastName: string;
    bio: string;
    photo: FileList | null;
}

const ProfileEditPopUp = ({
    EditProfileHandler,
    EditProfileImageHandler,
    PopUpHandler,
    avatarUrl,
}: ProfileEditPopUpProps) => {
    const fileInputRef = useRef<HTMLInputElement | null>(null);
    const currentProfileImage = useProfileAvatar(avatarUrl);
    const [previewUrl, setPreviewUrl] = useState<string>(currentProfileImage);

    const {
        register,
        handleSubmit,
        reset,
        watch,
        formState: { errors, isSubmitting },
    } = useForm<ProfileForm>({
        defaultValues: {
            firstName: "",
            lastName: "",
            bio: "",
            photo: null,
        },
    });

    const { ref: photoInputRef, onChange: photoOnChange, ...photoFieldProps } = register("photo");
    const selectedPhoto = watch("photo");

    useEffect(() => {
        setPreviewUrl(currentProfileImage);
    }, [currentProfileImage]);

    useEffect(() => {
        if (!selectedPhoto || selectedPhoto.length === 0) {
            setPreviewUrl(currentProfileImage);
            return;
        }

        const file = selectedPhoto[0];
        const objectUrl = URL.createObjectURL(file);
        setPreviewUrl(objectUrl);

        return () => URL.revokeObjectURL(objectUrl);
    }, [selectedPhoto, currentProfileImage]);

    const onSubmit = async (data: ProfileForm) => {
        const profilePayload: UpdateProfileParams = {};

        if (data.firstName.trim()) {
            profilePayload.firstName = data.firstName.trim();
        }

        if (data.lastName.trim()) {
            profilePayload.lastName = data.lastName.trim();
        }

        if (data.bio.trim()) {
            profilePayload.bio = data.bio.trim();
        }

        const hasProfileUpdate = Object.keys(profilePayload).length > 0;

        if (hasProfileUpdate) {
            await EditProfileHandler(profilePayload);
        }

        if (data.photo && data.photo.length > 0) {
            const imagePayload: UploadProfileImageParams = {
                photo: data.photo[0],
            };

            await EditProfileImageHandler(imagePayload);
        }

        PopUpHandler();
    };

    return (
        <div className={styles.overlay}>
            <form onSubmit={handleSubmit(onSubmit)} className={styles.popUp}>
                <button
                    type="button"
                    className={styles.closeButton}
                    onClick={PopUpHandler}
                    aria-label={PROFILE_CLOSE_ARIA_LABEL}
                >
                    ×
                </button>

                <h1 className={styles.title}>{PROFILE_POPUP_TITLE}</h1>

                <div className={styles.avatarSection}>
                    <button
                        type="button"
                        className={styles.avatarButton}
                        onClick={() => fileInputRef.current?.click()}
                        aria-label="Upload profile photo"
                    >
                        <img
                            src={previewUrl}
                            alt="Profile preview"
                            className={styles.avatarPreview}
                        />
                    </button>

                    <input
                        {...photoFieldProps}
                        ref={(element) => {
                            photoInputRef(element);
                            fileInputRef.current = element;
                        }}
                        type="file"
                        accept={PROFILE_PHOTO_ACCEPT}
                        className={styles.hiddenInput}
                        onChange={(event) => {
                            photoOnChange(event);
                        }}
                    />
                </div>

                <div className={styles.fieldGroup}>
                    <div className={styles.nameField}>
                        <Input
                            {...register("firstName")}
                            error={Boolean(errors.firstName)}
                            type="text"
                            borderStyle="dashed"
                            label={PROFILE_FORM_LABELS.firstName}
                            placeholder={PROFILE_FORM_PLACEHOLDERS.firstName}
                        />
                    </div>

                    <div className={styles.nameField}>
                        <Input
                            {...register("lastName")}
                            error={Boolean(errors.lastName)}
                            type="text"
                            borderStyle="dashed"
                            label={PROFILE_FORM_LABELS.lastName}
                            placeholder={PROFILE_FORM_PLACEHOLDERS.lastName}
                        />
                    </div>
                </div>

                <div className={styles.bio}>
                    <Input
                        {...register("bio")}
                        error={Boolean(errors.bio)}
                        type="text"
                        borderStyle="dashed"
                        label={PROFILE_FORM_LABELS.bio}
                        placeholder={PROFILE_FORM_PLACEHOLDERS.bio}
                    />
                </div>

                <div className={styles.actions}>
                    <Button
                        type="button"
                        variant="secondary"
                        onClick={() => {
                            reset();
                            setPreviewUrl(currentProfileImage);
                        }}
                        disabled={isSubmitting}
                    >
                        {PROFILE_FORM_BUTTONS.reset}
                    </Button>

                    <Button type="submit" disabled={isSubmitting}>
                        {isSubmitting ? PROFILE_FORM_BUTTONS.saving : PROFILE_FORM_BUTTONS.save}
                    </Button>
                </div>
            </form>
        </div>
    );
};

export default ProfileEditPopUp;