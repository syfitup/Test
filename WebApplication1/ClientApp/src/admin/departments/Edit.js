import React, { useState, useEffect } from 'react';
import { DepartmentClient } from '../../services/data/departmentClient';


function EditDepartment() {

    const department = {
        name: '',
        description: '',
        code: ''
    };

    const [userName, setUsername] = useState('JD');
    const [firstName, setFirstname] = useState('John');
    const [lastName, setLastname] = useState('Doe');
    const [model, setModel] = useState(department);

    const departmentClient = new DepartmentClient();


    useEffect(() => {
        departmentClient.getById("7FE7C550-8FB4-E711-B0AE-28F10E00516D").then(response => {
            setModel(response.data);
        });
    });

    const logName = () => {
        // do whatever with the names ...
        console.log(this.state.userName);
        console.log(this.state.firstName);
        console.log(this.state.lastName);
    };

    const handleUserNameInput = e => {
        setUsername({ userName: e.target.value });
    };
    const handleFirstNameInput = e => {
        setFirstname({ firstName: e.target.value });
    };
    const handleLastNameInput = e => {
        setLastname({ lastName: e.target.value });
    };

    return (
        <div>
            <h3> The text fields will update in 5 seconds </h3>
            <input
                type="text"
                onChange={handleUserNameInput}
                value={model.name}
                placeholder="Your username"
            />
            <input
                type="text"
                onChange={handleFirstNameInput}
                value={model.description}
                placeholder="Your firstname"
            />
            <input
                type="text"
                onChange={handleLastNameInput}
                value={model.code}
                placeholder="Your lastname"
            />
            <button className="btn btn-large right" onClick={logName}>
                {' '}
                Log Names{' '}
            </button>
        </div>
    );
};

export default EditDepartment;