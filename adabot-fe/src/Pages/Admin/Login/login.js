import React, { Component } from 'react';
import { auth } from '../../../Services/auth';

import './login.css';

const initialState = {}

class LoginAdminPage extends Component {

  constructor(props){
    super(props);
    this.state= initialState;
    this.login = this.login.bind(this);
  }

  componentWillMount() {
    if(auth.getAuthorizedUser()) {
      this.props.history.replace('/admin/resultados');
    }
  }

  login(event) {
    event.preventDefault();

    const user = {
      email: 'email@email.com',
      password: '1234'
    };

    auth.login(user)
    .then(response => {
      auth.setAuthorizedUser(user);
      this.setState({error: ""})
      this.props.history.replace('/admin/resultados');
    })
    .catch(error => {
      console.log("error", error.response)
    })
  }

  render() {
    return (
      <div>
        login
        <button onClick={e => this.login(e)}>Login</button>
      </div>
    );
  }
}

export {LoginAdminPage};
