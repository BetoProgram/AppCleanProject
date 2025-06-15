import api from "@/config/ApiAxios";
import type { ServicesResponse } from "@/types";

export class ServiceService{
    static async getAllServices(){
        const { data } = await api.get<ServicesResponse[]>('/services');
        return data;
    }
}