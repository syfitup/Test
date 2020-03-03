import React, { useState, useEffect, setState } from 'react';
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

    let columns = [
        { key: "id", name: "ID", editable: true },
        { key: "title", name: "Title", editable: true },
        { key: "complete", name: "Complete", editable: true }
    ];

    let rows = [
        { id: 0, title: "Task 1", complete: 20 },
        { id: 1, title: "Task 2", complete: 40 },
        { id: 2, title: "Task 3", complete: 60 }
    ];

    const test = "";
    const timesheetsClient = new TimesheetsClient();
    const [rowss, setModel] = useState(rows);
    const [columnss, setColmuns] = useState(columns);

    useEffect(() => {
            fetch();
    }, test);

    const onGridRowsUpdated = ({ fromRow, toRow, updated }) => {
            const updatedRows = rowss.slice();
            for (let i = fromRow; i <= toRow; i++) {
                updatedRows[i] = { ...rowss[i], ...updated };
        }

        setModel(updatedRows);

        return updatedRows;

    };

    const onChange = e => {
        const updateModel = { ...rowss };
        updateModel[e.target.name] = e.target.value;
        setModel(updateModel);
    };

    function fetch() {
        timesheetsClient.search().then(response => {
         
            let colData = [{ key: "0", name: "" }];


            for (var i = 1; i < response.data.length; i++) {
                colData.push({
                    key: i.toString(),
                    name: response.data[i].timesheetDate,
                    editable: true
                });

                if (i > 3) {
                    break;
                }
            }

            //Match the exact number of rows of rows otherwise the grid will not load property.

            let rows = [
                { 0: "Sipho"},
                { 0: "Khumalo"},
                { 0: "Weakness"}
            ];

            setColmuns(colData);
          

            setModel(rows);
        });
    };

    function save() {
        timesheetsClient.save(rowss).then(response => {
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
                columns={columnss}
                rowGetter={i => rowss[i]}
                rowsCount={3}
                enableCellSelect={true}
                onGridRowsUpdated={onGridRowsUpdated}
            />
        </div>
    );
};

export default Timesheets;