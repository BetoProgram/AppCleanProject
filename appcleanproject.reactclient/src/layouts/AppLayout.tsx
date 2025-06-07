import { Outlet } from "react-router-dom";
import { HeaderTopBar } from "@/components";

export default function AppLayout() {
  return (
    <div>
        <HeaderTopBar />
        <Outlet />
    </div>
  )
}
