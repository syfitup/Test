import React, { useState } from 'react';

function EditDepartment() {

    const [name, setName] = useState('John Doe');

    const alertName = () => {
        alert(name);
    };

    const handleNameInput = e => {
        setName(e.target.value);
    };

    return (
        <div>
            <h3> This is a Functional Component </h3>
            <input
                type="text"
                onChange={handleNameInput}
                value={name}
                placeholder="Your name"
            />
            <button onClick={alertName}> Alert </button>
        </div>
    );
};

export default EditDepartment;