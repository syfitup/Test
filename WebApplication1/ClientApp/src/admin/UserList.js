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
                <h1>Weather forecast</h1>
                <p>This component demonstrates fetching data from the server and working with URL parameters.</p>
                {renderForecastsTable(this.props)}
                {renderPagination(this.props)}
            </div>
        );
    }
}

function renderForecastsTable(props) {
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

function renderPagination(props) {
    const prevStartDateIndex = (props.startDateIndex || 0) - 5;
    const nextStartDateIndex = (props.startDateIndex || 0) + 5;

    return <p className='clearfix text-center'>
        <Link className='btn btn-default pull-left' to={`/fetch-data/${prevStartDateIndex}`}>Previous</Link>
        <Link className='btn btn-default pull-right' to={`/fetch-data/${nextStartDateIndex}`}>Next</Link>
        {props.isLoading ? <span>Loading...</span> : []}
    </p>;
}

export default connect(
    state => state.adminActions,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(UserList);
