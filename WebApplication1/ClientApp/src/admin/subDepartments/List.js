import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../../actions/adminSubDepartmentActions';

class SubDepartmentList extends Component {
    componentDidMount() {
        // This method is called when the component is first added to the document
        this.ensureDataFetched();
    }

    ensureDataFetched() {
        this.props.getSubDepartments();
    }

    render() {
        return (
            <div>
                <h1>Sub Departments Information</h1>
                {renderSubDepartmentsTable(this.props)}
            </div>
        );
    }
}

function renderSubDepartmentsTable(props) {
    return (
        <table className='table table-striped'>
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Department</th>
                    <th>Description</th>
                </tr>
            </thead>
            <tbody>
                {props.subdepartments.map(subdepartment =>
                    <tr key={subdepartment.id}>
                        <td>{subdepartment.name}</td>
                        <td>{subdepartment.departmentName}</td> 
                        <td>{subdepartment.description}</td> 
                    </tr>
                )}
            </tbody>
        </table>
    );
}

export default connect(
    state => state.adminSubDepartmentActions,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(SubDepartmentList);
