import React from 'react';
import { Route, Redirect } from 'react-router-dom';
import { auth } from '../Services/auth';


export const ProtectedRoute = ({ component: Component, ...rest }) => (
  <Route {...rest} render={(props) => (
    auth.getAuthorizedUser()
      ? <Component {...props} />
      : <Redirect to='/admin' />
  )} />
);
