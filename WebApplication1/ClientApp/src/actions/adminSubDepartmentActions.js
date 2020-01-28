import { GET_SUB_DEPARTMENTS, RECEIVE_SUB_DEPARTMENTS } from './types';
const initialState = { subdepartments: [], isLoading: false };

export const actionCreators = {
    getSubDepartments: () => async (dispatch, getState) => {

        dispatch({ type: GET_SUB_DEPARTMENTS });

        const url = `/api/subdepartment/Search`;
        const response = await fetch(url);
        const subdepartments = await response.json();

        dispatch({ type: RECEIVE_SUB_DEPARTMENTS, subdepartments });
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

    return state;
};
