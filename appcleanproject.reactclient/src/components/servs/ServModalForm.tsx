import React,{ useEffect, useState } from 'react'
import { useForm } from 'react-hook-form'
import { Button } from "@/components/ui/button"
import {
  Dialog,
  DialogClose,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog"

import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { PlusIcon } from "lucide-react"
import { useMutation, useQueryClient } from '@tanstack/react-query'
import { ServiceService } from '@/services/ServiceService'
import type { ServiceRequest, ServicesResponse } from '@/types'

interface ServModalFormProps {
  service?: ServicesResponse,
  setService: React.Dispatch<React.SetStateAction<ServicesResponse | null>>
}


export function ServModalForm({ service, setService }: ServModalFormProps) {

  const initialForm: ServiceRequest = {
    id: 0,
    name: "",
    description: "",
    durationMinutes: 0,
    price: 0
  }

  const [isOpen, setIsOpen] = useState(false);
  const [ edit, setEdit ] = useState(false);
  const { register,reset,handleSubmit, setValue, formState: { errors } } = useForm({ defaultValues: initialForm });
  const queryClient = useQueryClient();

  const { mutate } = useMutation({
    mutationFn: ServiceService.saveService,
    onSuccess() {
      setIsOpen(false);
      reset();
      queryClient.invalidateQueries({ queryKey:['services'] });
    },
    onError(error) {
      alert(error)
    }
  });

  const { mutate:updateMutate } = useMutation({
    mutationFn: ServiceService.updateService,
    onSuccess() {
      setIsOpen(false);
      setEdit(false);
      reset();
      queryClient.invalidateQueries({ queryKey:['services'] });
    },
    onError(error) {
      alert(error)
    }
  });

  useEffect(() => {
    if(service){
      setIsOpen(true);
      setValue("id", service.id);
      setValue("name", service.name);
      setValue("description", service.description);
      setValue("durationMinutes", service.durationMinutes);
      setValue("price", service.price);

      setEdit(true);
      setService(null);
    }
  },[service])

  const closeDialog = () => {
    reset();
  }

  const handleForm = (formData: any) => {
    if(edit){
      updateMutate(formData);
    }else{
      mutate(formData);
    }
  }

  return (
    <Dialog open={isOpen} onOpenChange={setIsOpen}>
      <DialogTrigger asChild>
        <Button type="button">
          <PlusIcon />
          Agregar nuevo servicio
        </Button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-[425px]">
        <form onSubmit={handleSubmit(handleForm)}>
          <Input type="hidden" { ...register('id') } />
          <DialogHeader>
            <DialogTitle>Nuevo Servicio</DialogTitle>
            <DialogDescription>
              Puedes dar de alta un servicio veterinario
            </DialogDescription>
          </DialogHeader>
          <div className="grid gap-4">
            <div className="grid gap-3">
              <Label htmlFor="name-1">Nombre</Label>
              <Input type="text" id="name-1" {...register('name', { required: 'Nombre es requerido' })}  />
              {errors.name && (
                <p className="text-xs text-red-600">
                  {errors.name.message}
                </p>
              )}
            </div>
            <div className="grid gap-3">
              <Label htmlFor="desc">Descripcion</Label>
              <Input type="text" id="desc" {...register('description', { required: 'Descripcion es requerida' })} />
              {errors.description && (
                <p className="text-xs text-red-600">
                  {errors.description.message}
                </p>
              )}
            </div>

            <div className="grid gap-3">
              <Label htmlFor="duration">Duracion en Minutos</Label>
              <Input type="number" id="duration" {...register('durationMinutes', { required: 'Duracion es requerida' })}  />
              {errors.durationMinutes && (
                <p className="text-xs text-red-600">
                  {errors.durationMinutes.message}
                </p>
              )}
            </div>

            <div className="grid gap-3">
              <Label htmlFor="price">Precio</Label>
              <Input type="number" id="price"step="any" {...register('price', { required: 'Precio es requerido' })}  />
              {errors.price && (
                <p className="text-xs text-red-600">
                  {errors.price.message}
                </p>
              )}
            </div>
          </div>
          <DialogFooter className="mt-5">
            <DialogClose asChild>
              <Button onClick={closeDialog} variant="outline">Cancelar</Button>
            </DialogClose>
            <Button type="submit">Guardar cambios</Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  )
}
