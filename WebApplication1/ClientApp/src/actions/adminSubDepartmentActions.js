import { GET_SUB_DEPARTMENTS, RECEIVE_SUB_DEPARTMENTS, ADD_SUB_DEPARTMENT, RESPONSE_STATUS, DELETE_SUB_DEPARTMENT } from './types';
const initialState = { subdepartments: [], isLoading: false, responsestatus: "" };


export const actionCreators = {
    getSubDepartments: () => async (dispatch, getState) => {

        dispatch({ type: GET_SUB_DEPARTMENTS });

        const url = `/api/subdepartments`;
        const response = await fetch(url);
        const subdepartments = await response.json();

        dispatch({ type: RECEIVE_SUB_DEPARTMENTS, subdepartments });
    },

    createSubDepartments: newSubDepartment => async (dispatch, getState) => {
        dispatch({ type: ADD_SUB_DEPARTMENT });
        const url = `/api/subdepartments/register`;

        let options = {
            method: "POST",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                "name": newSubDepartment.name,
                "code": newSubDepartment.code,
                "description": newSubDepartment.description,
                "departmentId": newSubDepartment.departmentId,
            })
        };

        const response = await fetch(url, options);
        const responsestatus = await response.json();

        dispatch({ type: RESPONSE_STATUS, responsestatus });
    },

    deleteSubDepartment: id => async (dispatch, getState) => {
        dispatch({ type: DELETE_SUB_DEPARTMENT });
        const url = `/api/subdepartments/${id}`;

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
    }
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === GET_SUB_DEPARTMENTS) {
        return {
            ...state,
            isLoading: true
        };
    }

    if (action.type === RECEIVE_SUB_DEPARTMENTS) {
        return {
            ...state,
            subdepartments: action.subdepartments,
            isLoading: false
        };
    }

    if (action.type === ADD_SUB_DEPARTMENT) {
        return {
            ...state,
            isLoading: false
        };
    }

    if (action.type === RESPONSE_STATUS) {
        return {
            ...state,
            responsestatus: action.responsestatus,
            isLoading: false
        };
    }

    return state;
};
