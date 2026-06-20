import Input from "../Input/Input";
import Button from "../Button/Button";
import styles from "./ProfilePopUp.module.css";

import { useForm } from "react-hook-form";

import type {
    UpdateProfileParams,
    UploadProfileImageParams
} from "../../services/profileService";

import {
    PROFILE_POPUP_TITLE,
    PROFILE_CLOSE_ARIA_LABEL,
    PROFILE_PHOTO_ACCEPT,
    PROFILE_FORM_LABELS,
    PROFILE_FORM_PLACEHOLDERS,
    PROFILE_FORM_ERRORS,
    PROFILE_FORM_BUTTONS
} from "./constants";

interface ProfileEditPopUpProps {
    EditProfileHandler: (
        profile: UpdateProfileParams
    ) => Promise<void>;

    EditProfileImageHandler: (
        image: UploadProfileImageParams
    ) => Promise<void>;

    PopUpHandler: () => void;
}

interface ProfileForm {
    name: string;
    bio: string;
    photo: FileList | null;
}

const ProfileEditPopUp = ({
    EditProfileHandler,
    EditProfileImageHandler,
    PopUpHandler
}: ProfileEditPopUpProps) => {
    const {
        register,
        handleSubmit,
        reset,
        watch,
        formState: {
            errors,
            isSubmitting
        }
    } = useForm<ProfileForm>({
        defaultValues: {
            name: "",
            bio: ""
        }
    });

    const selectedPhoto = watch("photo");

    const onSubmit = async (
        data: ProfileForm
    ) => {
        const nameParts = data.name
            .trim()
            .split(" ");

        const firstName =
            nameParts[0] ?? "";

        const lastName =
            nameParts.slice(1).join(" ");

        const profilePayload: UpdateProfileParams = {
            firstName,
            lastName,
            bio: data.bio
        };

        await EditProfileHandler(
            profilePayload
        );

        if (
            data.photo &&
            data.photo.length > 0
        ) {
            const imagePayload: UploadProfileImageParams =
            {
                photo: data.photo[0]
            };

            await EditProfileImageHandler(
                imagePayload
            );
        }

        PopUpHandler();
    };

    return (
        <div className={styles.overlay}>
            <form
                onSubmit={handleSubmit(
                    onSubmit
                )}
                className={styles.popUp}
            >
                <button
                    type="button"
                    className={
                        styles.closeButton
                    }
                    onClick={
                        PopUpHandler
                    }
                    aria-label={PROFILE_CLOSE_ARIA_LABEL}
                >
                    ×
                </button>

                <h1 className={styles.title}>
                    {PROFILE_POPUP_TITLE}
                </h1>

                <div className={styles.name}>
                    <Input
                        {...register(
                            "name",
                            {
                                required:
                                    PROFILE_FORM_ERRORS.nameRequired
                            }
                        )}
                        error={
                            errors.name
                                ?.message
                        }
                        type="text"
                        borderStyle="dashed"
                        label={PROFILE_FORM_LABELS.name}
                        placeholder={PROFILE_FORM_PLACEHOLDERS.name}
                    />
                </div>

                <div className={styles.bio}>
                    <Input
                        {...register(
                            "bio",
                            {
                                required:
                                    PROFILE_FORM_ERRORS.bioRequired
                            }
                        )}
                        error={
                            errors.bio
                                ?.message
                        }
                        type="text"
                        borderStyle="dashed"
                        label={PROFILE_FORM_LABELS.bio}
                        placeholder={PROFILE_FORM_PLACEHOLDERS.bio}
                    />
                </div>

                <div className={styles.file}>
                    <Input
                        type="file"
                        accept={PROFILE_PHOTO_ACCEPT}
                        borderStyle="dashed"
                        label={
                            selectedPhoto?.[0]
                                ?.name
                                ? `Selected: ${selectedPhoto[0].name}`
                                : PROFILE_FORM_LABELS.photo
                        }
                        error={
                            errors.photo
                                ?.message
                        }
                        {...register(
                            "photo"
                        )}
                    />
                </div>

                <div
                    className={
                        styles.actions
                    }
                >
                    <Button
                        type="button"
                        variant="secondary"
                        onClick={() =>
                            reset()
                        }
                        disabled={
                            isSubmitting
                        }
                    >
                        {PROFILE_FORM_BUTTONS.reset}
                    </Button>

                    <Button
                        type="submit"
                        disabled={
                            isSubmitting
                        }
                    >
                        {isSubmitting
                            ? PROFILE_FORM_BUTTONS.saving
                            : PROFILE_FORM_BUTTONS.save}
                    </Button>
                </div>
            </form>
        </div>
    );
};

export default ProfileEditPopUp;