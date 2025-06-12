import { jwtDecode } from 'jwt-decode';

type DecodedToken = {
    exp:number;
}

export function tokenValid(token:string):boolean{
    try {
        const decoded = jwtDecode<DecodedToken>(token!);
        return decoded.exp * 1000 > Date.now();
    } catch (error) {
        console.log('invalid token', error);
        return false
    }
}