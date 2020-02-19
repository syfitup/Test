import axios, { AxiosResponse } from 'axios';

export class DepartmentClient {
    getById(id: string): any {
        return axios.get(`/api/departments/${id}`);
    }

    search(criteria: any):any {
        return axios.get("/api/departments", { params: criteria });
    }

    create(model: any): any{
        return axios.post("/api/departments", model);
    }

    update(id: string, model): any {
        return axios.put(`/api/departments/${id}`, model);
    }

    delete(id: string):any {
        return axios.delete(`/api/departments/${id}`);
    }
}