import { Link, useNavigate } from "react-router-dom";
import {
  Card,
  CardAction,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card"

//import { Badge } from "@/components/ui/badge"
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { Button } from "@/components/ui/button";
import { PetsService } from "@/services/PetsService";
import AlertOpenDialog from "@/components/shared/alert-confirm-dialog";
import type { PetResponse } from "@/types";

export default function ListPetsPage() {
  const navigate = useNavigate();
  const queryClient = useQueryClient();
  const { data: pets } = useQuery({ queryKey: ['pets'], queryFn: PetsService.getAllPets });

  const {mutate:deleteMutate} = useMutation({
    mutationFn: PetsService.removePet,
    onSuccess(){
      queryClient.invalidateQueries({ queryKey:['pets'] })
    },
    onError(){

    }
  });


  const goToUpdatePet = (pet:any) => {
    navigate('/pets/updated',{ state: { pet: pet } });
  }

  const confirmation = (id:PetResponse['id']) => {
      deleteMutate(id);
  }

  return (
    <div>
      <h2 className="text-2xl font-bold mb-4">Tus Mascotas</h2>
      <div className="mb-4">
        <Button asChild>
          <Link to="/pets/register">Registrar Mascota</Link>
        </Button>
      </div>
      <div className="grid grid-cols-3 gap-4">
        {pets?.map((pet) => (
          <Card className="@container/card" key={pet.id}>
            <CardHeader>
              <CardDescription>Imagen</CardDescription>
              <CardTitle className="text-2xl font-semibold tabular-nums @[250px]/card:text-3xl">
                { pet.name }
              </CardTitle>
              <CardAction>
                {/* <Badge variant="outline">
                  
                </Badge> */}
              </CardAction>
            </CardHeader>
            <CardFooter className="flex-col items-start gap-1.5 text-sm">
              <div className="line-clamp-1 flex gap-2">
                <span className="font-medium">Raza:</span> {pet.breed}
              </div>
              <div className="line-clamp-1 flex gap-2">
                <span className="font-medium">Fecha de Nacimiento:</span> {pet.dateOfBirth}
              </div>
              <div className="line-clamp-1 flex gap-2">
                <span className="font-medium">Especie:</span> {pet.species}
                <span className="font-medium">Genero:</span> {pet.gender}
              </div>

              <div className="line-clamp-1 flex gap-2">
                <span className="font-medium">Descripción: </span> 
                <span className="text-muted-foreground">
                  {pet.characteristics}
                </span>
              </div>
              
            </CardFooter>
            <div className="mt-auto flex justify-between pl-3 pr-3">
                <Button onClick={() => goToUpdatePet(pet)}>
                  Editar
                </Button>
                <AlertOpenDialog onConfirm={() => confirmation(pet.id)} key={pet.id} />
            </div>
          </Card>
        ))}
      </div>
      
    </div>

  )
}
