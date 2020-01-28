import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../../actions/adminDepartmentActions';

class DepartmentList extends Component {
    componentDidMount() {
        // This method is called when the component is first added to the document
        this.ensureDataFetched();
    }

    ensureDataFetched() {
        this.props.getDepartments();
    }

    render() {
        return (
            <div>
                <h1>Departments Information</h1>
                {renderForecastsTable(this.props)}
            </div>
        );
    }
}

function renderForecastsTable(props) {
    return (
        <table className='table table-striped'>
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Description</th>
                    <th>Sub Department (s)</th>
                </tr>
            </thead>
            <tbody>
                {props.departments.map(department => 
                    <tr key={department.id}>
                        <td>{department.name}</td>
                        <td>{department.description}</td>
                        <td>{department.departmentName}</td>
                    </tr>
                )}
            </tbody>
        </table>
    );
}

export default connect(
    state => state.adminDepartmentActions,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(DepartmentList);
