import Input from "../Input/Input";
import Button from "../Button/Button";
import styles from "./ProfilePopUp.module.css";

import { useForm } from "react-hook-form";

import type {
    UpdateProfileParams,
    UploadProfileImageParams
} from "../../services/profileService";

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
                    aria-label="Close popup"
                >
                    ×
                </button>

                <h1 className={styles.title}>
                    Edit Your Profile
                </h1>

                <div className={styles.name}>
                    <Input
                        {...register(
                            "name",
                            {
                                required:
                                    "Name is required"
                            }
                        )}
                        error={
                            errors.name
                                ?.message
                        }
                        type="text"
                        borderStyle="dashed"
                        label="Name"
                        placeholder="Enter your new name"
                    />
                </div>

                <div className={styles.bio}>
                    <Input
                        {...register(
                            "bio",
                            {
                                required:
                                    "Bio is required"
                            }
                        )}
                        error={
                            errors.bio
                                ?.message
                        }
                        type="text"
                        borderStyle="dashed"
                        label="Bio"
                        placeholder="Enter your new bio"
                    />
                </div>

                <div className={styles.file}>
                    <Input
                        type="file"
                        accept="image/*"
                        borderStyle="dashed"
                        label={
                            selectedPhoto?.[0]
                                ?.name
                                ? `Selected: ${selectedPhoto[0].name}`
                                : "Upload Photo"
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
                        variant="purple"
                        onClick={() =>
                            reset()
                        }
                        disabled={
                            isSubmitting
                        }
                    >
                        Reset
                    </Button>

                    <Button
                        type="submit"
                        variant="purple"
                        disabled={
                            isSubmitting
                        }
                    >
                        {isSubmitting
                            ? "Saving..."
                            : "Save"}
                    </Button>
                </div>
            </form>
        </div>
    );
};

export default ProfileEditPopUp;