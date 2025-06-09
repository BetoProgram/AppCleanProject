import type { AuthUserResponse } from '@/types';
import { create } from 'zustand';

interface AuthUserState {
    userAuth: AuthUserResponse | null;
    isAuthenticated: boolean;
    isValidToken: boolean;
    setUser: (user: AuthUserResponse) => void,
    setToken: (token:string) => void,
    clearUser: () => void
}

export const useAuthStore = create<AuthUserState>()((set) => ({
    userAuth:null,
    isAuthenticated: false,
    isValidToken: false,
    setUser(user) {
        localStorage.setItem('user', JSON.stringify(user));
        set({ userAuth:user });
    },
    setToken(token) {
        
    },
    clearUser() {
        set({ userAuth:null });
        set({ isAuthenticated:false });
        localStorage.removeItem('user');
    },
}));