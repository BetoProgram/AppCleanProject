import api from "@/config/ApiAxios";
import type { ServiceRequest, ServicesResponse } from "@/types";

export class ServiceService{
    static async getAllServices(){
        const { data } = await api.get<ServicesResponse[]>('/services');
        return data;
    }

    static async saveService(form:ServiceRequest){
        await api.post('/services', form);
    }

    static async updateService(form:ServicesResponse){
        await api.put(`/services/${form.id}`, form);
    }

    static async activateService(param:{ id:ServiceRequest['id'], isActive:boolean }){
        await api.patch(`/services/activate`, param);
    }
}