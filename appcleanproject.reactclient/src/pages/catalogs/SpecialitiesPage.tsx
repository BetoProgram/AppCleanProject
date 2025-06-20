import { SpecialitiesService } from "@/services/SpecialitiesService";
import { useQuery } from "@tanstack/react-query";

import {
  Table,
  TableBody,
  TableCaption,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { SpecialitiesModalForm } from "@/components/servs/SpecialitiesModalForm";

export default function SpecialitiesPage() {
  const { data: specialities } = useQuery({ queryKey: ['specialities'], queryFn: SpecialitiesService.getAllSpecialities });

  return (
    <div>
      <div className="mb-4">
        <SpecialitiesModalForm />
      </div>
      <Table>
        <TableCaption>Lista de Especialidades</TableCaption>
        <TableHeader>
          <TableRow>
            <TableHead className="w-[100px]">Id</TableHead>
            <TableHead>Nombre</TableHead>
            <TableHead>Descripcion</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          { specialities?.map((spec) => (
            <TableRow key={spec.id}>
              <TableCell className="font-medium">{spec.id}</TableCell>
              <TableCell>{spec.name}</TableCell>
              <TableCell>{spec.description}</TableCell>
            </TableRow>
          ))}
          
        </TableBody>
      </Table>
    </div>
  )
}
