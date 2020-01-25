import { GET_DEPARTMENTS, RECEIVE_DEPARTMENTS } from './types';
const initialState = { users: [], isLoading: false };

export const actionCreators = {
    getDepartments: () => async (dispatch, getState) => {

        dispatch({ type: GET_DEPARTMENTS });

        const url = `/api/department/Search`;
        const response = await fetch(url);
        const departments = await response.json();

        dispatch({ type: RECEIVE_DEPARTMENTS, departments });
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
            users: action.departments,
            isLoading: false
        };
    }

    return state;
};
