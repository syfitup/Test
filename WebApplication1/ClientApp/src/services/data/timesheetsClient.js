import axios, { AxiosResponse } from 'axios';

export class TimesheetsClient {
    search(criteria):any {
        return axios.get("/api/timesheets", { params: criteria });
    }

    save(model): any {
        return axios.post("/api/timesheets", model);
    }
}