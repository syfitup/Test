import React, { Component } from 'react';
import { connect } from 'react-redux';
import TextFieldGroup from '../common/TextFieldGroup';
import SelectListGroup from '../common/SelectListGroup';
import { actionCreators } from '../actions/authActions';
import { bindActionCreators } from 'redux';


class Register extends Component {
    constructor() {
        super();
        this.state = {
            name: '',
            email: '',
            password: '',
            password2: '',
            userName: '',
            phoneNumber: '',
            mobileNumber: '',
            departmentId: '',
            subDepartmentId: '',
            disabled: '',
            errors: {}
        };

        this.onChange = this.onChange.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
    }

    componentDidMount() {
    }

    UNSAFE_componentWillReceiveProps(nextProps) {
        if (nextProps.errors) {
            this.setState({ errors: nextProps.errors });
        }
    }

    onChange(e) {
        this.setState({ [e.target.name]: e.target.value });
    }

    onSubmit(e) {
        e.preventDefault();

        const newUser = {
            name: this.state.name,
            email: this.state.email,
            password: this.state.password,
            password2: this.state.password2,
            userName: this.state.userName,
            phoneNumber: this.state.phoneNumber,
            mobileNumber: this.state.mobileNumber,
            departmentId: this.state.departmentId,
            subDepartmentId: this.state.subDepartmentId,
            disabled: '',
        };

        this.props.registerUser(newUser);
    }

    render() {

        // Select options for status
        const departmentOptions = [
            { label: '* Select Department', value: 0 },
            { label: 'Safety', value: '7FE7C550-8FB4-E711-B0AE-28F10E00516D' },
            { label: 'Other', value: '80E7C550-8FB4-E711-B0AE-28F10E00516D' },
            { label: 'Mining', value: '81E7C550-8FB4-E711-B0AE-28F10E00516D' },
            { label: 'Engineering', value: '82E7C550-8FB4-E711-B0AE-28F10E00516D' },
            { label: 'Processing', value: '83E7C550-8FB4-E711-B0AE-28F10E00516D' },
            { label: 'Engineering', value: '84E7C550-8FB4-E711-B0AE-28F10E00516D' },
            { label: 'Site', value: '85E7C550-8FB4-E711-B0AE-28F10E00516D' },
            { label: 'Supply Chain', value: '88E7C550-8FB4-E711-B0AE-28F10E00516D' }
        ];

        // Select options for status
        const subDepartmentOptions = [
            { label: '* Select a Sub Department', value: 0 },
            { label: 'Safety - Sub', value: '9D5248ED-752B-E811-A978-124AF894EC66' },
            { label: 'Other - Sub', value: '26D4FFF7-752B-E811-A978-124AF894EC66' },
            { label: 'Mining - Sub', value: 'DA4CE49A-2E58-E811-A978-124AF894EC66' },
            { label: 'Engineering - Sub', value: '502AEABF-2E58-E811-A978-124AF894EC66' },
            { label: 'Processing - Sub', value: 'B5EAF9EF-2E58-E811-A978-124AF894EC66' },
            { label: 'Engineering - Sub', value: 'C9C5881E-E467-E811-A979-124AF894EC66' },
            { label: 'Site - Sub', value: 'EE0D4228-E467-E811-A979-124AF894EC66' },
            { label: 'Supply Chain - Sub', value: '743CF02E-E467-E811-A979-124AF894EC66' }
        ];

        const { errors } = this.state;

        return (
            <div className="register">
                <div className="container">
                    <div className="row">
                        <div className="col-md-8 m-auto">
                            <h1 className="display-4 text-center">Sign Up</h1>
                            <p className="lead text-center">
                                Create your DevConnector account
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
                                    placeholder="Email"
                                    name="email"
                                    type="email"
                                    value={this.state.email}
                                    onChange={this.onChange}
                                    error={errors.email}
                                    info="This site uses Gravatar so if you want a profile image, use a Gravatar email"
                                />
                                <TextFieldGroup
                                    placeholder="UserName"
                                    name="userName"
                                    value={this.state.userName}
                                    onChange={this.onChange}
                                    error={errors.userName}
                                    info="The name to use when logging in!"
                                />
                                <TextFieldGroup
                                    placeholder="PhoneNumber"
                                    name="phoneNumber"
                                    value={this.state.phoneNumber}
                                    onChange={this.onChange}
                                    error={errors.phoneNumber}
                                    info="Please add your phone number"
                                />
                                <TextFieldGroup
                                    placeholder="MobileNumber"
                                    name="mobileNumber"
                                    value={this.state.mobileNumber}
                                    onChange={this.onChange}
                                    error={errors.mobileNumber}
                                    info="Please Add your Mobile Number"
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
                                <SelectListGroup
                                    placeholder="subDepartmentId"
                                    name="subDepartmentId"
                                    value={this.state.subDepartmentId}
                                    onChange={this.onChange}
                                    options={subDepartmentOptions}
                                    error={errors.subDepartmentId}
                                    info="Please select your Sub Department"
                                />
                                <TextFieldGroup
                                    placeholder="Password"
                                    name="password"
                                    type="password"
                                    value={this.state.password}
                                    onChange={this.onChange}
                                    error={errors.password}
                                />
                                <TextFieldGroup
                                    placeholder="Confirm Password"
                                    name="password2"
                                    type="password"
                                    value={this.state.password2}
                                    onChange={this.onChange}
                                    error={errors.password2}
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

const mapStateToProps = state => ({
    auth: state.auth,
    errors: state.errors
});

export default connect(
    state => state.weatherForecasts,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(Register);
