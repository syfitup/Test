import axios, { AxiosResponse } from 'axios';

export class DepartmentClient {
    getById(id): any {
        return axios.get(`/api/departments/${id}`);
    }

    search(criteria):any {
        return axios.get("/api/departments", { params: criteria });
    }

    create(model): any{
        return axios.post("/api/departments", model);
    }

    update(id, model): any {
        return axios.put(`/api/departments/${id}`, model);
    }

    delete(id ):any {
        return axios.delete(`/api/departments/${id}`);
    }
}