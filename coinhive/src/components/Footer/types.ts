
export interface FooterItem {
    groupName: string;
    groupOptions: string[]
}
export interface FooterProps {
    logo: string;
    tagline: string;
    items: FooterItem[];
    copyright: string;

}