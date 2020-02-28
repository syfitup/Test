import React, { useState, useEffect } from 'react';
import ReactDataGrid from "react-data-grid";
import { TimesheetsClient } from '../../services/data/timesheetsClient';
import TextFieldGroup from '../../common/TextFieldGroup';
import "./list.css";

function Timesheets() {

    const criteria = {
        PersonId: '',
        fromDate: Date.now(),
        periods: 7
    };

    const columns = [
        { key: "id", name: "ID", editable: true },
        { key: "title", name: "Title", editable: true },
        { key: "complete", name: "Complete", editable: true }
    ];

    const rows = [
        { id: 0, title: "Task 1", complete: 20 },
        { id: 1, title: "Task 2", complete: 40 },
        { id: 2, title: "Task 3", complete: 60 }
    ];

    const test = "";
    const [model, setModel] = useState(criteria);
    const timesheetsClient = new TimesheetsClient();

    useEffect(() => {
            fetch();
    }, test);

    const onGridRowsUpdated = ({ fromRow, toRow, updated }) => {
        this.setState(state => {
            const rows = state.rows.slice();
            for (let i = fromRow; i <= toRow; i++) {
                rows[i] = { ...rows[i], ...updated };
            }
            return { rows };
        });
    };

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
        <div className="App">
            <ReactDataGrid
                columns={columns}
                rowGetter={i => rows[i]}
                rowsCount={3}
                enableCellSelect={true}
                onGridRowsUpdated={onGridRowsUpdated}
                minHeight={650}
                minWidth={650}
            />
        </div>
    );
};

export default Timesheets;