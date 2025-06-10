import { Controller, useForm } from 'react-hook-form'
import { useNavigate } from 'react-router-dom'
import { Button } from "@/components/ui/button"
import {
    Card,
    CardContent,
    //CardDescription,
    CardHeader,
    CardTitle,
} from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import {
    RadioGroup,
    RadioGroupItem,
} from "@/components/ui/radio-group"

import { AuthService } from '@/services/AuthService'
import { toast } from 'sonner'

export default function RegisterPage() {

    const navigate = useNavigate();

    const initialForm = {
        email: "",
        password: "",
        firstName: "",
        lastName: "",
        phoneNumber: "",
        tipoRol:2
    }

    const { register,control, handleSubmit, formState: { errors } } = useForm({ defaultValues: initialForm });

    const handleForm = async (formData: any) => {
        await AuthService.register(formData).then(() => {
            toast("Se ha registrado correctamente el usuario");
            navigate('/login');
        });
    }

    return (
        <div className="flex flex-col gap-6">
            <Card>
                <CardHeader>
                    <CardTitle>Register to your user</CardTitle>
                </CardHeader>
                <CardContent>
                    <form onSubmit={handleSubmit(handleForm)}>
                        <div className="flex flex-col gap-6">
                            <div className="grid gap-3">
                                <Label htmlFor="firstName">First Name</Label>
                                <Input
                                    id="firstName"
                                    type="text"
                                    placeholder="FirstName"
                                    {...register("firstName", {
                                        required: "First Name is required",
                                    })}
                                />

                                {errors.firstName && (
                                    <p className="text-xs text-red-600">
                                        {errors.firstName.message}
                                    </p>
                                )}
                            </div>
                            <div className="grid gap-3">
                                <Label htmlFor="lastName">Last Name</Label>
                                <Input
                                    id="lastName"
                                    type="text"
                                    placeholder="Last Name"
                                    {...register("lastName", {
                                        required: "Last Name is required",
                                    })}
                                />

                                {errors.lastName && (
                                    <p className="text-xs text-red-600">
                                        {errors.lastName.message}
                                    </p>
                                )}
                            </div>
                            <div className="grid gap-3">
                                <Label htmlFor="phoneNumber">Phone Number</Label>
                                <Input
                                    id="phoneNumber"
                                    type="text"
                                    placeholder="Phone Number"
                                    {...register("phoneNumber", {
                                        required: "Phone Number is required",
                                    })}
                                />

                                {errors.phoneNumber && (
                                    <p className="text-xs text-red-600">
                                        {errors.phoneNumber.message}
                                    </p>
                                )}
                            </div>
                            <div className="grid gap-3">
                                <Label htmlFor="email">Email</Label>
                                <Input
                                    id="email"
                                    type="email"
                                    placeholder="m@example.com"
                                    {...register("email", {
                                        required: "Email is required",
                                        pattern: {
                                            value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                                            message: "Email invalid"
                                        }
                                    })}
                                />

                                {errors.email && (
                                    <p className="text-xs text-red-600">
                                        {errors.email.message}
                                    </p>
                                )}
                            </div>
                            <div className="grid gap-3">
                                <div className="flex items-center">
                                    <Label htmlFor="password">Password</Label>
                                    {/*  <a
                    href="#"
                    className="ml-auto inline-block text-sm underline-offset-4 hover:underline"
                  >
                    Forgot your password?
                  </a> */}
                                </div>
                                <Input id="password" type="password"
                                    {...register("password", { required: "Password is required" })}
                                />


                                {errors.password && (
                                    <p className="text-xs text-red-600">
                                        {errors.password.message}
                                    </p>
                                )}
                            </div>
                            <div className="grid gap-3">
                                <Controller name="tipoRol" 
                                control={control} rules={ { required: 'Tipo Rol is required' }}
                                render={({field}) => (

                                    <RadioGroup defaultValue={field.value.toString()} onValueChange={(value) => {
                                        field.onChange(value)
                                    }}>
                                        <div className="flex items-center gap-3">
                                            <RadioGroupItem value="3" id="r1" />
                                            <Label htmlFor="r1">Client</Label>
                                        </div>
                                        <div className="flex items-center gap-3">
                                            <RadioGroupItem value="2" id="r2" />
                                            <Label htmlFor="r2">Veterinarian</Label>
                                        </div>
                                    </RadioGroup>
                                )}
                                />
                                
                                {errors.tipoRol && (
                                    <p className="text-xs text-red-600">
                                        {errors.tipoRol.message}
                                    </p>
                                )}
                            </div>
                            <div className="flex flex-col gap-3">
                                <Button type="submit" className="w-full">
                                    Save
                                </Button>
                            </div>
                        </div>
                        <div className="mt-4 text-center text-sm">
                            Don&apos;t have an account?{" "}
                            <a href="#" className="underline underline-offset-4">
                                Sign in
                            </a>
                        </div>
                    </form>
                </CardContent>
            </Card>
        </div>
    )
}