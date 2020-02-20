import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../../actions/adminDepartmentActions';

class TimesheetList extends Component {
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
                <h1>Employee Timesheets</h1>
                {this.renderTable(this.props)}
            </div>
        );
    }

    renderTable(props) {
        return (
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Code</th>
                        <th>Description</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {props.departments.map(department =>
                        <tr key={department.id}>                        
                            <td>{department.name}</td>
                            <td>{department.code}</td>
                            <td>{department.description}</td>
                            <Link to={{ pathname: "/department", id: department.id }}> Edit </Link>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }
}

export default connect(
    state => state.adminDepartmentActions,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(TimesheetList);
