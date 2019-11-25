import { GET_ERRORS, SET_CURRENT_USER } from './types';

// Register User
export const registerUser = (userData, history) => dispatch => {
    //axios
    //    .post('/api/users/register', userData)
    //    .then(res => history.push('/login'))
    //    .catch(err =>
    //        dispatch({
    //            type: GET_ERRORS,
    //            payload: err.response.data
    //        })
    //    );
};

export const actionCreators = {
    loginUser: userData => async (dispatch, getState) => {

        const url = `/api/user/authenticate`;
        let options = {
            method: "POST",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                "email": userData.email,
                "password": userData.password
            })
        };
        const response = await fetch(url, options);
        const loginStatus = await response.json();

        dispatch({ type: SET_CURRENT_USER, loginStatus });
    },

    registerUser: newUser => async (dispatch, getState) => {

        const url = `/api/user/register`;
        let options = {
            method: "POST",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                "emailAddress": newUser.email,
                "password": newUser.password,
                "userName": newUser.userName,
                "name": newUser.name,
                "phoneNumber": newUser.phoneNumber,
                "mobileNumber": newUser.mobileNumber,
                "departmentId": newUser.departmentId,
                "subDepartmentId": newUser.subDepartmentId,
            })
        };

        const response = await fetch(url, options);
        const registerStatus = await response.json();

        dispatch({ type: SET_CURRENT_USER, registerStatus });
    }
};


// Set logged in user
export const setCurrentUser = decoded => {
    return {
        type: SET_CURRENT_USER,
        payload: decoded
    };
};

// Log user out
export const logoutUser = () => dispatch => {
    // Remove token from localStorage
    localStorage.removeItem('jwtToken');
    // Remove auth header for future requests
    //setAuthToken(false);
    // Set current user to {} which will set isAuthenticated to false
    dispatch(setCurrentUser({}));
};
