import api from "@/config/ApiAxios";
import type { AuthLoginRequest, AuthRegisterRequest, AuthUserResponse } from "@/types";
import { isAxiosError } from "axios";
import { toast } from "sonner";

export class AuthService {
    static async login(form: AuthLoginRequest){
       try {
            const { data } = await api.post<AuthUserResponse>('auth/login', form);
            return data;
       } catch (error) {
            if(isAxiosError(error) && error.response){
                toast("Error: "+  error.response.data.errors.message);
            }
       }
    }

    static async register(form: AuthRegisterRequest){
        try {
            const { data } = await api.post('auth/register', form);
            return data;
       } catch (error) {
            if(isAxiosError(error) && error.response){
                toast("Error: "+  error.response.data.errors.message);
            }
       }
    }
}