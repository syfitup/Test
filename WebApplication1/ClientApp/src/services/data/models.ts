
export interface DepartmentSearchRequest {
    term?: string;
}

export interface DepartmentModel {
    id?: string | undefined;
    code?: string | undefined;
    name?: string | undefined;
    description?: string | undefined;
}

export interface DepartmentSearchModel {
    id?: string | undefined;
    code?: string | undefined;
    name?: string | undefined;
    description?: string | undefined;
}

export interface CreateResponse {
    id?: any | undefined;
    code?: string | undefined;
}