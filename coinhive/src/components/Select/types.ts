
export type Option = {
    label: string;
    value: string;
};
export interface CustomSelectProps {
    value?: string;
    onChange?: (value: string) => void;
    options: Option[];
    placeholder?: string;
    variant: 'primary'|'secondary';
}
