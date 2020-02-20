import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../actions/adminActions';

class UserList extends Component {
    componentDidMount() {
        // This method is called when the component is first added to the document
        this.ensureDataFetched();
    }

    ensureDataFetched() {
        this.props.getUsers();
    }

    render() {
        return (
            <div>
                <h1>Employees</h1>
                {renderTable(this.props)}
                <Link to={{ pathname: "/register"}}> Add Employee </Link>
            </div>
        );
    }
}

function renderTable(props) {
    return (
        <table className='table table-striped'>
            <thead>
                <tr>
                    <th>Username</th>
                    <th>Name</th>
                    <th>Department</th>
                    <th>Sub Department</th>
                    <th>Email Address</th>
                    <th>Group</th>
                    <th>Position</th>
                </tr>
            </thead>
            <tbody>
                {props.users.map(user =>
                    <tr key={user.id}>
                        <td>{user.userName}</td>
                        <td>{user.name}</td>
                        <td>{user.departmentName}</td>
                        <td>{user.subDepartmentName}</td>
                        <td>{user.emailAddress}</td>
                        <td>{user.groupName}</td>
                        <td>{user.personPositionName}</td>
                    </tr>
                )}
            </tbody>
        </table>
    );
}

export default connect(
    state => state.adminActions,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(UserList);
