import * as React from "react"
import {
  Clock,
  BriefcaseMedical,
  PawPrint,
  ChartArea,
  LayoutList,
  Gem
} from "lucide-react"

import { NavMain } from "@/components/shared/nav-main"
import { NavProjects } from "@/components/shared/nav-projects"
import { NavUser } from "@/components/shared/nav-user"
import { TeamSwitcher } from "@/components/shared/team-switcher"
import {
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarHeader,
  SidebarRail,
} from "@/components/ui/sidebar"
import { useAuthStore } from "@/stores/authStore"

// This is sample data.
const data = {
  user: {
    name: "VeteSis",
    email: "m@example.com",
    avatar: "/avatars/shadcn.jpg",
  },
  teams: [
    {
      name: "VeteSis Inc",
      logo: PawPrint,
      plan: "Enterprise",
    }
  ],
  navMain: [
    {
      title: "Mascotas",
      url: "#",
      icon: PawPrint,
      items: [
        {
          title: "Ver Mascotas",
          url: "/pets",
        },
        {
          title: "Registro",
          url: "/pets/register",
        },
      ],
    },
    {
      title: "Veterinarios",
      url: "#",
      icon: BriefcaseMedical,
      items: [
        {
          title: "Ver Veterinarios",
          url: "#",
        },
        {
          title: "Asigna Horario de Trabajo",
          url: "#",
        },
      ],
    },
    {
      title: "Citas",
      url: "#",
      icon: Clock,
      items: [
        {
          title: "Generar Cita",
          url: "#",
        },
        {
          title: "Confirmación Citas",
          url: "#",
        },
        {
          title: "Ver Citas",
          url: "#",
        },
      ]
    },
    {
      title: "Reportes",
      url: "#",
      icon: ChartArea,
      items: [
        {
          title: "Citas",
          url: "#",
        },
        {
          title: "Mascotas",
          url: "#",
        },
        {
          title: "Servicios",
          url: "#",
        },
      ],
    },
  ],
  projects: [
    {
      name: "Servicios",
      url: "/catalog/services",
      icon: LayoutList,
    },
    {
      name: "Especialidades",
      url: "/catalog/specialities",
      icon: Gem,
    },
  ],
}

export function AppSidebar({ ...props }: React.ComponentProps<typeof Sidebar>) {
  const store = useAuthStore()

  data.user.email = store.userAuth?.email!

  return (
    <Sidebar collapsible="icon" {...props}>
      <SidebarHeader>
        <TeamSwitcher teams={data.teams} />
      </SidebarHeader>
      <SidebarContent>
        <NavMain items={data.navMain} />
        <NavProjects projects={data.projects} />
      </SidebarContent>
      <SidebarFooter>
        <NavUser user={data.user} />
      </SidebarFooter>
      <SidebarRail />
    </Sidebar>
  )
}
