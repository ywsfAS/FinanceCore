export const PROFILE_POPUP_TITLE = "Edit Your Profile";

export const PROFILE_CLOSE_ARIA_LABEL = "Close popup";

export const PROFILE_PHOTO_ACCEPT = "image/*";

export const PROFILE_FORM_LABELS = {
    name: "Name",
    bio: "Bio",
    photo: "Upload Photo"
} as const;

export const PROFILE_FORM_PLACEHOLDERS = {
    name: "Enter your new name",
    bio: "Enter your new bio"
} as const;

export const PROFILE_FORM_ERRORS = {
    nameRequired: "Name is required",
    bioRequired: "Bio is required"
} as const;

export const PROFILE_FORM_BUTTONS = {
    reset: "Reset",
    save: "Save",
    saving: "Saving..."
} as const;