import { GET_USERS, RECEIVE_USERS } from './types';
const initialState = { users: [], isLoading: false };

export const actionCreators = {
    getUsers: () => async (dispatch) => {

        dispatch({ type: GET_USERS });

        const url = `/api/user/Search`;
        const response = await fetch(url);
        const users = await response.json();

        dispatch({ type: RECEIVE_USERS, users });
    }
    
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === GET_USERS) {
        return {
            ...state,
            isLoading: true
        };
    }

    if (action.type === RECEIVE_USERS) {
        return {
            ...state,
            users: action.users,
            isLoading: false
        };
    }

    return state;
};
