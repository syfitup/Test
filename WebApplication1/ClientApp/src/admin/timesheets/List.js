import React, { useState, useEffect } from 'react';
import { TimesheetsClient } from '../../services/data/timesheetsClient';
import TextFieldGroup from '../../common/TextFieldGroup';

function Timesheets() {

    const criteria = {
        PersonId: '',
        fromDate: Date.now(),
        periods: 7
    };

    const test = "";
    const [model, setModel] = useState(criteria);
    const timesheetsClient = new TimesheetsClient();

    useEffect(() => {
            fetch();
    }, test);

    const onChange = e => {
        const updateModel = { ...model };
        updateModel[e.target.name] = e.target.value;
        setModel(updateModel);
    };

    function fetch() {
        timesheetsClient.search().then(response => {
            setModel(response.data);
        });
    };

    function save() {
            timesheetsClient.save(model).then(response => {
                var test = response;
            });
    };

    const onSubmit = e => {
        e.preventDefault();
        save();
    };

    return (
        <div className="register">
            <div className="container">
                <div className="row">
                    <div className="col-md-8 m-auto">
                        <h1 className="display-4 text-center">Department</h1>
                        <p className="lead text-center">
                            Edit Department
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

export default Timesheets;