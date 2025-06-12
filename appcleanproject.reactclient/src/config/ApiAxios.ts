import globalRouter from '@/lib/globalRouter';
import axios from 'axios';

const api = axios.create({
    baseURL: import.meta.env.VITE_URL_API
});

api.interceptors.request.use(config => {
    const user = JSON.parse(localStorage.getItem("user")!);

    if(user.token){
        config.headers.Authorization = `Bearer ${user.token}`
    }

    return config;
});

api.interceptors.response.use(response => {
    return response;
}, error => {
    if(error.response.status === 401){
        localStorage.removeItem("token")
        globalRouter.navigate!("/auth/login")
    }
    return Promise.reject(error);
});


export default api;