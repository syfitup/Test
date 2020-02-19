import React, { Component } from 'react';
import { connect } from 'react-redux';
import TextFieldGroup from '../../common/TextFieldGroup';
import SelectListGroup from '../../common/SelectListGroup';
import { actionCreators } from '../../actions/adminSubDepartmentActions';
import { bindActionCreators } from 'redux';


class SubDepartmentEdit extends Component {
    constructor(props) {
        super(props);
        this.state = {
            name: '',
            code: '',
            description: '',
            departmentId: '',
            disabled: '',
            errors: {}
        };

        this.onChange = this.onChange.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
    }

    componentDidMount() {

        if (this.props) {
            this.fetch(this.props.location.subdepartment.id, this.props.location.subdepartment.newRecord);
        }
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps) {
            this.setState({ errors: nextProps.errors });
        }
    }

    fetch(id, newRecord) {
        if (newRecord === "true") {
            this.setState({
                subdepartment: {
                    name: '',
                    code: '',
                    description: '',
                    departmentId: '',
                    disabled: '',
                    errors: {}
                }
            });
        } else {
            this.props.getSubDepartment(id).then(response => {
                this.setState({ subdepartment: response });
            });
        }
    }

    onChange(e) {
        this.setState({ [e.target.name]: e.target.value });
    }

    onSubmit(e) {
        e.preventDefault();

        const newSubDepartment = {
            name: this.state.name,
            code: this.state.code,
            description: this.state.description,
            departmentId: this.state.departmentId,
            disabled: '',
        };

       this.props.createSubDepartments(newSubDepartment);
    }

    render() {

        // Select options for status
        const departmentOptions = [
            { label: '* Select Department', value: 0 },
            { label: 'Safety', value: '7FE7C550-8FB4-E711-B0AE-28F10E00516D' },
            { label: 'Other', value: '80E7C550-8FB4-E711-B0AE-28F10E00516D' },
            { label: 'Mining', value: '81E7C550-8FB4-E711-B0AE-28F10E00516D' },
            { label: 'Processing', value: '83E7C550-8FB4-E711-B0AE-28F10E00516D' },
            { label: 'Engineering', value: '84E7C550-8FB4-E711-B0AE-28F10E00516D' },
            { label: 'Site', value: '85E7C550-8FB4-E711-B0AE-28F10E00516D' },
            { label: 'Supply Chain', value: '88E7C550-8FB4-E711-B0AE-28F10E00516D' }
        ];

        const { errors, subdepartment } = this.state;

        return (
            <div className="register">
                <div className="container">
                    <div className="row">
                        <div className="col-md-8 m-auto">
                            <h1 className="display-4 text-center">Sign Up</h1>
                            <p className="lead text-center">
                                Add a Sub Department
                            </p>
                            <form noValidate onSubmit={this.onSubmit}>
                                <TextFieldGroup
                                    placeholder="Name"
                                    name="name"
                                    value={this.state.name}
                                    onChange={this.onChange}
                                    error={errors.name}
                                />
                                <TextFieldGroup
                                    placeholder="Code"
                                    name="code"
                                    value={this.state.code}
                                    onChange={this.onChange}
                                    error={errors.code}
                                />
                                <TextFieldGroup
                                    placeholder="Description"
                                    name="description"
                                    value={this.state.description}
                                    onChange={this.onChange}
                                    error={errors.description}
                                    info="The name to use when logging in!"
                                />
                                <SelectListGroup
                                    placeholder="DepartmentId"
                                    name="departmentId"
                                    value={this.state.departmentId}
                                    onChange={this.onChange}
                                    options={departmentOptions}
                                    error={errors.departmentId}
                                    info="Please select your department"
                                />

                                <input type="submit" className="btn btn-info btn-block mt-4" />
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}


export default connect(
    state => state.adminSubDepartmentActions,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(SubDepartmentEdit);
