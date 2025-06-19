import {
  Card,
  CardAction,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card"

import { Badge } from "@/components/ui/badge"
import { ServiceService } from "@/services/ServiceService";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { Button } from "@/components/ui/button";
import { ServModalForm } from "@/components/servs/ServModalForm";
import { useState } from "react";
import type { ServicesResponse } from "@/types";

export default function ServicesPage() {
  const [ service, setService ] = useState<ServicesResponse | null>(null);
  const queryClient = useQueryClient();
  const { data: services } = useQuery({ queryKey: ['services'], queryFn: ServiceService.getAllServices });

  const { mutate } = useMutation({
    mutationFn: ServiceService.activateService,
    onSuccess() {
      queryClient.invalidateQueries({ queryKey:['services'] });
    }
  });

  const activeService = (service:ServicesResponse) => {
    mutate({ id:service.id, isActive: !service.isActive })
  }

  return (
    <div>
      <h2 className="text-2xl font-bold mb-4">Lista de Servicios</h2>
      <div className="mb-4">
        <ServModalForm service={service!} setService={setService} />
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
                <Button disabled={ !service.isActive } onClick={() => setService(service)}>Editar</Button>
                <Button onClick={() => activeService(service)}>
                  { service.isActive ? 'Desactivar': 'Activar' }
                </Button>
              </div>
          </Card>
        ))}
      </div>
    </div>

  )
}
