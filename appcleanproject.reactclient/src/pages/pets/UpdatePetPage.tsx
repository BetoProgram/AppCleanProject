import { useEffect, useState } from "react";
import { useNavigate, useLocation, Link } from 'react-router-dom'
import { Controller, useForm } from "react-hook-form";
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { Textarea } from "@/components/ui/textarea"
import { ChevronDownIcon } from "lucide-react";
import type { PetUpdateRequest } from "@/types";

import { Calendar } from "@/components/ui/calendar"
import {
    Popover,
    PopoverContent,
    PopoverTrigger,
} from "@/components/ui/popover"

import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select"
import { formatDate } from "@/lib/formatDate";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { PetsService } from "@/services/PetsService";

export default function UpdatePetPage() {
    const location = useLocation()
    const pet = location.state?.pet;
    const initialForm: PetUpdateRequest = {
        id: 0,
        name: '',
        species: '',
        breed: '',
        dateOfBirth: '',
        gender: '',
        photoUrl: null,
        characteristics: ''
    }
    const navigate = useNavigate();
    const [open, setOpen] = useState(false)
    const [date, setDate] = useState<Date | undefined>(undefined)
    const { register, control, reset, handleSubmit, setValue, formState: { errors } } = useForm({ defaultValues: initialForm });
    const queryClient = useQueryClient();

    useEffect(() => {
        if (pet) {
            setValue('id', pet.id)
            setValue('name', pet.name)
            setValue('species', pet.species)
            setValue('breed', pet.breed)
            setValue('gender',pet.gender, { shouldValidate:true, shouldDirty:true })
            setValue('characteristics', pet.characteristics)
            setDate(new Date(pet.dateOfBirth))

            if(!open){
                setValue('dateOfBirth', pet.dateOfBirth)
            }
        }
    },[pet])

    const { mutate: updateMutate } = useMutation({
        mutationFn: PetsService.updatePet,
        onSuccess() {
            queryClient.invalidateQueries({ queryKey: ['pets'] })
            reset()
            navigate('/pets')
        }
    })

    const handleForm = (formData: PetUpdateRequest) => {
        updateMutate(formData)
    }

    return (
        <>
            <div className="mb-3">
                <h1 className="text-2xl font-bold">Edita los datos de tu mascota</h1>
            </div>
            <form onSubmit={handleSubmit(handleForm)}>
                <Input type="hidden" { ...register('id') } />
                <div className="grid grid-cols-3 gap-4 p-4">
                    <div className="grid gap-3">
                        <Label htmlFor="name-1">Nombre</Label>
                        <Input type="text" id="name-1" {...register('name', { required: 'Nombre es requerido' })} />
                        {errors.name && (
                            <p className="text-xs text-red-600">
                                {errors.name.message}
                            </p>
                        )}
                    </div>
                    <div className="grid gap-3">
                        <Label htmlFor="spec-1">Especie</Label>
                        <Input type="text" id="spec-1" {...register('species', { required: 'La especie es requerida' })} />
                        {errors.species && (
                            <p className="text-xs text-red-600">
                                {errors.species.message}
                            </p>
                        )}
                    </div>
                    <div className="grid gap-3">
                        <Label htmlFor="breed-1">Raza</Label>
                        <Input type="text" id="breed-1" {...register('breed', { required: 'La raza es requerida' })} />
                        {errors.breed && (
                            <p className="text-xs text-red-600">
                                {errors.breed.message}
                            </p>
                        )}
                    </div>
                </div>
                <div className="grid grid-cols-3 gap-4 p-4">
                    <div className="grid gap-3">
                        <Label htmlFor="date-1">Fecha de Nacimiento</Label>
                        <Popover open={open} onOpenChange={setOpen}>
                            <PopoverTrigger asChild>
                                <Button
                                    variant="outline"
                                    id="date"
                                    className="w-48 justify-between font-normal"
                                >
                                    {date ? date.toLocaleDateString() : "Selecccionar Fecha"}
                                    <ChevronDownIcon />
                                </Button>
                            </PopoverTrigger>
                            <PopoverContent className="w-auto overflow-hidden p-0" align="start">
                                <Calendar
                                    mode="single"
                                    selected={date}
                                    captionLayout="dropdown"
                                    onSelect={(date) => {
                                        setDate(date)
                                        const dateFormat = formatDate(date!)
                                        setValue('dateOfBirth', dateFormat)
                                        setOpen(false)
                                    }}
                                />
                            </PopoverContent>
                        </Popover>
                    </div>
                    <div className="grid gap-3">
                        <Label htmlFor="gen-1">Genero</Label>
                        <Controller
                            name="gender"
                            control={control}
                            rules={{ required: 'El genero es requerido' }}
                            render={({ field }) => (
                                <Select onValueChange={field.onChange} value={field.value}>
                                    <SelectTrigger id="gender" className="w-full">
                                        <SelectValue placeholder="Seleccione Genero" />
                                    </SelectTrigger>
                                    <SelectContent>
                                        <SelectItem value="macho">Macho</SelectItem>
                                        <SelectItem value="hembra">Hembra</SelectItem>
                                        <SelectItem value="indefinido">Indefinido</SelectItem>
                                    </SelectContent>
                                </Select>
                            )}
                        />
                        {errors.gender && (
                            <p className="text-xs text-red-600">
                                {errors.gender.message}
                            </p>
                        )}
                    </div>
                    <div className="grid gap-3">
                        <Label htmlFor="ph-1">Subir Foto</Label>
                        <Input type="file" id="ph-1" />
                    </div>
                </div>
                <div className="grid grid-cols-3 gap-4 p-4">
                    <div className="col-span-2 grid gap-3">
                        <Label htmlFor="name-1">Descripción</Label>
                        <Textarea placeholder="Caracteristicas" {...register('characteristics')} />
                    </div>
                </div>
                <div className="grid grid-cols-2 gap-4 p-4">
                    <Button type="submit">Editar</Button>
                    <Button asChild>
                        <Link to="/pets">Regresar</Link>
                    </Button>
                </div>
            </form>
        </>

    )
}
