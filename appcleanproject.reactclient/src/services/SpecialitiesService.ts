import api from "@/config/ApiAxios";
import type { SpecialitiesRequest, SpecialitiesResponse } from "@/types";

export class SpecialitiesService {
    static async getAllSpecialities(){
        const { data } = await api.get<SpecialitiesResponse[]>('/specialties');
        return data;
    }

    static async saveSpecialities(form:SpecialitiesRequest){
        await api.post('/specialties', form);
    }
}