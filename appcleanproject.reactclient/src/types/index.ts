export interface AuthBase {
    email:string;
    password:string;
    token:string;
    firstName:string;
    lastName:string;
    phoneNumber:string;
}

export interface ServicesResponse {
    id?:number;
    name:string;
    description:string;
    durationMinutes:number;
    price:number;
    isActive:boolean;
}

export interface SpecialitiesResponse {
    id:number;
    name:string;
    description:string;
}

export interface PetResponse {
    id:number;
    ownerId:number;
    name:string;
    species:string;
    breed?:string;
    dateOfBirth:string;
    gender?:string;
    characteristics?:string;
    photoUrl?:string | null;
    createdAt?:string;
    updatedAt?:string;
}

export type ServiceRequest = Omit<ServicesResponse, 'isActive'>;
export type SpecialitiesRequest = Omit<SpecialitiesResponse, 'id'>;
export type PetRequest = Omit<PetResponse, 'id' | 'ownerId' | 'createdAt'| 'updatedAt'>;
export type PetUpdateRequest = Omit<PetResponse, 'ownerId' | 'createdAt'| 'updatedAt'>;

export type AuthRegisterRequest = Omit<AuthBase, 'token'>;
export type AuthLoginRequest = Pick<AuthBase, 'email' | 'password'>;
export type AuthUserResponse = Pick<AuthBase, 'email' | 'token'>;