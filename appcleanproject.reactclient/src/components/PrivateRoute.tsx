import type { ReactNode } from "react"
import { Navigate, useNavigate } from "react-router-dom";
import { useAuthStore } from '@/stores/authStore';
import globalRouter from "@/lib/globalRouter";

type PrivateRouteProps = {
    children: ReactNode
}

export default function PrivateRoute({ children, ...rest }:PrivateRouteProps) {
     const navigate = useNavigate();
    globalRouter.navigate = navigate;
    const verifyToken = useAuthStore((state) => state.verifyAuth);
    const isAuthenticated = useAuthStore((state) => state.isAuthenticated);
    const isValidToken = useAuthStore((state) => state.isValidToken);
    verifyToken();

    return !isAuthenticated && !isValidToken ? <Navigate to="/auth/login" /> :children;
}
