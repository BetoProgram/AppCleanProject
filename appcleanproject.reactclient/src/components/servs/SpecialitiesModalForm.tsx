import { useState } from 'react'
import { useMutation, useQueryClient } from '@tanstack/react-query'
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
import { SpecialitiesService } from '@/services/SpecialitiesService'
import type { SpecialitiesRequest } from '@/types'


export function SpecialitiesModalForm() {

  const initialForm: SpecialitiesRequest = {
    name: "",
    description: ""
  }

  const [isOpen, setIsOpen] = useState(false);
  const { register,reset,handleSubmit, formState: { errors } } = useForm({ defaultValues: initialForm });
  const queryClient = useQueryClient();

  const { mutate } = useMutation({
    mutationFn: SpecialitiesService.saveSpecialities,
    onSuccess() {
      setIsOpen(false);
      reset();
      queryClient.invalidateQueries({ queryKey:['specialities'] });
    },
    onError(error) {
      alert(error)
    }
  });


  const closeDialog = () => {
    reset();
  }

  const handleForm = (formData: any) => {
     mutate(formData);
  }

  return (
    <Dialog open={isOpen} onOpenChange={setIsOpen}>
      <DialogTrigger asChild>
        <Button type="button">
          <PlusIcon />
          Agregar nueva especialidad
        </Button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-[425px]">
        <form onSubmit={handleSubmit(handleForm)}>
          <DialogHeader>
            <DialogTitle>Nueva Especialidad</DialogTitle>
            <DialogDescription>
              Puedes dar de alta una especialidad del Veterinario
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
          </div>
          <DialogFooter className="mt-5">
            <DialogClose asChild>
              <Button onClick={closeDialog} variant="outline">Cancelar</Button>
            </DialogClose>
            <Button type="submit">Guardar</Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  )
}
