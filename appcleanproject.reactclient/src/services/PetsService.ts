import api from "@/config/ApiAxios";
import type { PetRequest, PetResponse, PetUpdateRequest } from "@/types";

export class PetsService {
    static async getAllPets(){
        const { data } = await api.get<PetResponse[]>('/Pets');
        return data;
    }

    static async savePet(form:PetRequest){
        await api.post('/Pets', form);
    }

    static async updatePet(form:PetUpdateRequest){
        await api.put(`/pets/${form.id}`, form);
    }
}