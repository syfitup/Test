import { GET_DEPARTMENTS, RECEIVE_DEPARTMENTS, ADD_DEPARTMENT, RESPONSE_STATUS, DELETE_DEPARTMENT, GET_DEPARTMENT, RECEIVE_DEPARTMENT } from './types';
const initialState = { departments: [], isLoading: false };

export const actionCreators = {
    getDepartments: () => async (dispatch, getState) => {

        dispatch({ type: GET_DEPARTMENTS });

        const url = `/api/departments`;
        const response = await fetch(url);
        const departments = await response.json();

        dispatch({ type: RECEIVE_DEPARTMENTS, departments });
    },

    createDepartment: newDepartment => async (dispatch, getState) => {
        dispatch({ type: ADD_DEPARTMENT });
        const url = `/api/subdepartments/register`;

        let options = {
            method: "POST",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                "name": newDepartment.name,
                "code": newDepartment.code,
                "description": newDepartment.description,
                "departmentId": newDepartment.departmentId,
            })
        };

        const response = await fetch(url, options);
        const responsestatus = await response.json();

        dispatch({ type: RESPONSE_STATUS, responsestatus });
    },

    deleteDepartment: id => async (dispatch, getState) => {
        dispatch({ type: DELETE_DEPARTMENT });
        const url = `/api/departments/${id}`;

        let options = {
            method: "DELETE",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            }
        };

        const response = await fetch(url, options);
        const responsestatus = await response.json();

        dispatch({ type: RESPONSE_STATUS, responsestatus });
    },

    getDepartment: id => async (dispatch, getState) => {
        dispatch({ type: GET_DEPARTMENT });
        const url = `/api/departments/${id}`;

        let options = {
            method: "GET",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            }
        };

        const response = await fetch(url, options);
        const department = await response.json();

        dispatch({ type: RECEIVE_DEPARTMENT, department });
    }
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === GET_DEPARTMENTS) {
        return {
            ...state,
            isLoading: true
        };
    }

    if (action.type === RECEIVE_DEPARTMENTS) {
        return {
            ...state,
            departments: action.departments,
            isLoading: false
        };
    }

    if (action.type === GET_DEPARTMENT) {
        return {
            ...state,
            isLoading: true
        };
    }

    if (action.type === RECEIVE_DEPARTMENT) {
        return {
            ...state,
            department: action.department,
            isLoading: false
        };
    }

    return state;
};
