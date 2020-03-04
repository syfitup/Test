import { GET_USERS, RECEIVE_USERS } from './types';
const initialState = { users: [], isLoading: false };

export const actionCreators = {
    getUsers: startDateIndex => async (dispatch, getState) => {

        if (!getState().adminActions.users) {
            // Don't issue a duplicate request (we already have or are loading the requested data)
            var a = 0;
            return;
        }

        dispatch({ type: GET_USERS });

        const url = `/api/users`;
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
