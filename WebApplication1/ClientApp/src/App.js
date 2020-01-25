import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import Login from './account/Login';
import Register from './account/Register';
import UserList from './admin/UserList';
import DepartmentList from './admin/departments/List';

export default () => (
    <Layout>
        <Route exact path='/' component={Login} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetch-data/:startDateIndex?' component={FetchData} />
        <Route path='/login' component={Login} />
        <Route path='/register' component={Register} />
        <Route path='/user-list/:startDateIndex?' component={UserList} />
        <Route path='/department-list' component={DepartmentList} />
    </Layout>
);
