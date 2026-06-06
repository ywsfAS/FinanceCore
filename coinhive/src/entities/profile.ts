
export interface Profile{
    firstName: string,
    lastName: string,
    bio: string,
    role? : string,
    photo : File | string,
    currency: string
}