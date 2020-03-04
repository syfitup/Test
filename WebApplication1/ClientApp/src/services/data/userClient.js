import axios, { AxiosResponse } from 'axios';

export class UsersClient {
    search(criteria):any {
        return axios.get("/api/users", { params: criteria });
    }
}