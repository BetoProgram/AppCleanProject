export interface AuthBase {
    email:string;
    password:string;
    token:string;
    firstName:string;
    lastName:string;
    phoneNumber:string;
}

export type AuthRegisterRequest = Omit<AuthBase, 'token'>;
export type AuthLoginRequest = Pick<AuthBase, 'email' | 'password'>;
export type AuthUserResponse = Pick<AuthBase, 'email' | 'token'>;