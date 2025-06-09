import { useForm } from 'react-hook-form'
import { useNavigate } from 'react-router-dom'
import { Button } from "@/components/ui/button"
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { AuthService } from '@/services/AuthService'
import { useAuthStore } from '@/stores/authStore'

export default function LoginPage() {
  const setUser = useAuthStore((state) => state.setUser);
  const navigate = useNavigate();

  const initialForm = {
    email:"",
    password:""
  }

  const { register, handleSubmit, formState:{errors} } = useForm({ defaultValues: initialForm});

  const handleForm = async(formData:any) => {
    const user = await AuthService.login(formData);
    if(user){
      setUser(user);
      navigate('/');
    }
  }

  return (
    <div className="flex flex-col gap-6">
      <Card>
        <CardHeader>
          <CardTitle>Login to your account</CardTitle>
          <CardDescription>
            Enter your email below to login to your account
          </CardDescription>
        </CardHeader>
        <CardContent>
          <form onSubmit={handleSubmit(handleForm)}>
            <div className="flex flex-col gap-6">
              <div className="grid gap-3">
                <Label htmlFor="email">Email</Label>
                <Input
                  id="email"
                  type="email"
                  placeholder="m@example.com"
                  { ...register("email",{
                    required: "Email is required",
                    pattern:{ 
                      value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                      message: "Email invalid"
                    }
                  })}
                />

                { errors.email && (
                  <p className="text-xs text-red-600">
                    { errors.email.message }
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
                { ...register("password", { required: "Password is required" }) }
                />


                { errors.password && (
                  <p className="text-xs text-red-600">
                    { errors.password.message }
                  </p>
                )}
              </div>
              <div className="flex flex-col gap-3">
                <Button type="submit" className="w-full">
                  Login
                </Button>
              </div>
            </div>
            <div className="mt-4 text-center text-sm">
              Don&apos;t have an account?{" "}
              <a href="#" className="underline underline-offset-4">
                Sign up
              </a>
            </div>
          </form>
        </CardContent>
      </Card>
    </div>
  )
}