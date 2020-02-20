import React, { useState, useEffect } from 'react';
import { DepartmentClient } from '../../services/data/departmentClient';
import TextFieldGroup from '../../common/TextFieldGroup';

function EditDepartment(props) {

    const department = {
        name: '',
        description: '',
        code: ''
    };

    const test = "";
    const [model, setModel] = useState(department);
    const departmentClient = new DepartmentClient();

    useEffect(() => {
        if (props.location) {
            fetch(props.location.id);
        }
    }, test);

    const onChange = e => {
        const updateModel = { ...model };
        updateModel[e.target.name] = e.target.value;
        setModel(updateModel);
    };

    function fetch(id){
        departmentClient.getById(id).then(response => {
            setModel(response.data);
        });
    };

    function save() {
        if (props.location.id) {
            departmentClient.update(props.location.id, model).then(response => {
                var test = response;
            });
        }
        else {
            departmentClient.create(model).then(response => {
                var test = response;
            });
        }
    };

    const onSubmit = e =>  {
        e.preventDefault();
        save();
    };

    return (
        <div className="register">
            <div className="container">
                <div className="row">
                    <div className="col-md-8 m-auto">
                        <h1 className="display-4 text-center">Sign Up</h1>
                        <p className="lead text-center">
                            Add a Sub Department
                            </p>
                        <form noValidate onSubmit={onSubmit}>
                            <TextFieldGroup
                                placeholder="Name"
                                name="name"
                                value={model.name}
                                onChange={onChange}
                            />
                            <TextFieldGroup
                                placeholder="Code"
                                name="code"
                                value={model.code}
                                onChange={onChange}
                            />
                            <TextFieldGroup
                                placeholder="Description"
                                name="description"
                                value={model.description}
                                onChange={onChange}
                                info="The name to use when logging in!"
                            />

                            <input type="submit" className="btn btn-info btn-block mt-4" />
                        </form>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default EditDepartment;