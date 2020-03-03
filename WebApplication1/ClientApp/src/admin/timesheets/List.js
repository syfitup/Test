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
        { key: "personId", name: "ID", editable: true },
        { key: "timesheetDate", name: "Title", editable: true },
        { key: "timesheetEmployeeHours", name: "Complete", editable: true }
    ];

    let rows = [
        { personId: 0, timesheetDate: "Task 1", timesheetEmployeeHours: 20 },
        { personId: 1, timesheetDate: "Task 2", timesheetEmployeeHours: 40 },
        { personId: 2, timesheetDate: "Task 3", timesheetEmployeeHours: 60 }
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
        save();

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

        for (var i = 1; i < columnss.length; i++) {
            var columnKey = columnss[i].key;

            var columnData = columnKey;

            for (var k = 1; k < rowss.length; k++) {
                var rowKey = !!rowss[k][i] ? rowss[k][i]: null;

                var rowData = rowKey;
            }
        }

        var testSave = [{
            Id: "92855359-4d90-4909-8fcc-196c000ce9d0",
            personId: "92855359-4d90-4909-8fcc-196c000ce9d0",
            timesheetDate: new Date("2020-03-02"),
            timesheetEmployeeHours: 0
        }];

        timesheetsClient.save(testSave).then(response => {
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