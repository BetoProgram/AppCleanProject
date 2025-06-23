import {
  Card,
  CardAction,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card"

//import { Badge } from "@/components/ui/badge"
import { useQuery } from "@tanstack/react-query";
import { Button } from "@/components/ui/button";
import { PetsService } from "@/services/PetsService";
import { Link } from "react-router-dom";

export default function ListPetsPage() {
  const { data: pets } = useQuery({ queryKey: ['pets'], queryFn: PetsService.getAllPets });

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
                
            </div>
          </Card>
        ))}
      </div>
    </div>

  )
}
