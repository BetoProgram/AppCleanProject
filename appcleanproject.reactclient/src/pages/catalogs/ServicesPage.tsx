import {
  Card,
  CardAction,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card"

import { Badge } from "@/components/ui/badge"
import { PlusIcon } from "lucide-react"

import { ServiceService } from "@/services/ServiceService";
import { useQuery } from "@tanstack/react-query";
import { Button } from "@/components/ui/button";

export default function ServicesPage() {
  const { data: services } = useQuery({ queryKey: ['services'], queryFn: ServiceService.getAllServices });

  return (
    <div>
      <h2 className="text-2xl font-bold mb-4">Lista de Servicios</h2>
      <div className="mb-4">
        <Button>
          <PlusIcon />
          Agregar nuevo servicio
        </Button>
      </div>
      <div className="grid grid-cols-3 gap-4">
        {services?.map((service) => (
          <Card className="@container/card" key={service.id}>
            <CardHeader>
              <CardDescription>Precio</CardDescription>
              <CardTitle className="text-2xl font-semibold tabular-nums @[250px]/card:text-3xl">
                ${service.price}
              </CardTitle>
              <CardAction>
                <Badge variant="outline">
                  {service.durationMinutes} minutos
                </Badge>
              </CardAction>
            </CardHeader>
            <CardFooter className="flex-col items-start gap-1.5 text-sm">
              <div className="line-clamp-1 flex gap-2 font-medium">
                {service.name}
              </div>
              <div className="text-muted-foreground">
                {service.description}
              </div>
              
            </CardFooter>
            <div className="mt-auto flex justify-between pl-3 pr-3">
                <Button>Editar</Button>
                <Button>Activar</Button>
              </div>
          </Card>
        ))}
      </div>
    </div>

  )
}
