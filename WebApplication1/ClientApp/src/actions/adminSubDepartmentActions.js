import { GET_SUB_DEPARTMENTS, RECEIVE_SUB_DEPARTMENTS, ADD_SUB_DEPARTMENT, RESPONSE_STATUS } from './types';
const initialState = { subdepartments: [], isLoading: false, createstatus: "" };


export const actionCreators = {
    getSubDepartments: () => async (dispatch, getState) => {

        dispatch({ type: GET_SUB_DEPARTMENTS });

        const url = `/api/subdepartment/Search`;
        const response = await fetch(url);
        const subdepartments = await response.json();

        dispatch({ type: RECEIVE_SUB_DEPARTMENTS, subdepartments });
    },

    createSubDepartments: newSubDepartment => async (dispatch, getState) => {
        dispatch({ type: ADD_SUB_DEPARTMENT });
        const url = `/api/subdepartment/register`;

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
        const createStatus = await response.json();

        dispatch({ type: RESPONSE_STATUS, createStatus });
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
            createstatus: action.createstatus,
            isLoading: false
        };
    }

    return state;
};
