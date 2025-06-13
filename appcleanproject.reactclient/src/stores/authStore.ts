import type { AuthUserResponse } from '@/types';
import { tokenValid } from '@/lib/tokenValid';
import { create } from 'zustand';

interface AuthUserState {
    token: string | null,
    userAuth: AuthUserResponse | null;
    isAuthenticated: boolean;
    isValidToken: boolean;
    setUser: (user: AuthUserResponse) => void,
    setToken: (token:string) => void,
    clearUser: () => void,
    verifyAuth: () => void
}

export const useAuthStore = create<AuthUserState>()((set) => ({
    token: '',
    userAuth:null,
    isAuthenticated: false,
    isValidToken: false,
    setUser:(user) => {
        localStorage.setItem('user', JSON.stringify(user));
        set({ userAuth:user });
        set({ isAuthenticated: true });
    },
    setToken:(token) => {
        set({ token });
        set({ isAuthenticated: !!token })
    },
    clearUser:() => {
        set({ userAuth:null });
        set({ isAuthenticated:false });
        localStorage.removeItem('user');
    },
    verifyAuth: () => {
        const objectString = localStorage.getItem("user")!;
        const user = JSON.parse(objectString);

        if(user.token){
            const validToken = tokenValid(user.token);

            if(!validToken){
                set({ isValidToken: false })
                set({ isAuthenticated: false })
            }else{
                set({ isValidToken: true })
                set({ isAuthenticated: true })
            }
            
        }else{
            set({ isAuthenticated: false })
        }
    }
}));