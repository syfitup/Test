import React, { useState, useEffect, setState } from 'react';
import ReactDataGrid from "react-data-grid";
import { TimesheetsClient } from '../../services/data/timesheetsClient';
import { UsersClient } from '../../services/data/userClient';
import TextFieldGroup from '../../common/TextFieldGroup';
import "./list.css";

function Timesheets() {

    const criteria = {
        PersonId: '',
        fromDate: Date.now(),
        periods: 7
    };

    let columns = [
        { key: "personId", name: "ID", editable: true },
        { key: "timesheetDate", name: "Title", editable: true },
        { key: "timesheetEmployeeHours", name: "Complete", editable: true }
    ];

    let rows = [
        { personId: "Admin", timesheetDate: "Date Time", timesheetEmployeeHours: 20 },
        { personId: "Admin", timesheetDate: "Date Time", timesheetEmployeeHours: 40 },
        { personId: "Admin", timesheetDate: "Date Time", timesheetEmployeeHours: 60 }
    ];

    const test = "";
    const timesheetsClient = new TimesheetsClient();
    const usersClient = new UsersClient();
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
        save();

        return updatedRows;

    };

    const onChange = e => {
        const updateModel = { ...rowss };
        updateModel[e.target.name] = e.target.value;
        setModel(updateModel);
    };

    function fetch() {

        usersClient.search().then(response => {
            //Match the exact number of rows of rows otherwise the grid will not load property.
            let rows = [];

            for (var i = 0; i < response.data.length; i++) {
                rows.push({ 0: response.data[i].name });
            }

            setModel(rows);
        });

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

            setColmuns(colData);        
        });
    };

    function save() {
        var testSave = [];
        for (var i = 1; i < columnss.length; i++) {

            for (var k = 0; k < rowss.length; k++) {
                var timsheetDate = columnss[i].name;
                var timeEntry = !!rowss[k][i] ? parseFloat(rowss[k][i]) : null;

                testSave.push({
                    personName: rowss[k][0],
                    timesheetDate: new Date(timsheetDate),
                    timesheetEmployeeHours: timeEntry
                });
            }
        }

        timesheetsClient.save(testSave).then(response => {
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