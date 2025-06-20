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

export type ServiceRequest = Omit<ServicesResponse, 'isActive'>;
export type SpecialitiesRequest = Omit<SpecialitiesResponse, 'id'>;

export type AuthRegisterRequest = Omit<AuthBase, 'token'>;
export type AuthLoginRequest = Pick<AuthBase, 'email' | 'password'>;
export type AuthUserResponse = Pick<AuthBase, 'email' | 'token'>;