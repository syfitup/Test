import axios, { AxiosResponse } from 'axios';

export class TimesheetsClient {
    search(criteria):any {
        return axios.get("/api/timesheets", { params: criteria });
    }

    save(id, model): any {
        return axios.put("/api/timesheets", model);
    }
}