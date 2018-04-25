import React, { Component } from 'react';
import { auth } from '../../../Services/auth';
import isEmail from 'validator/lib/isEmail';

import './login.css';
import logo from '../../../assets/img/logo-techy.svg';
import { Button, FormGroup, InputGroup, FormControl } from 'react-bootstrap';

const initialState = {
  email: {
    value: '',
    focus: false,
    error: false
  },
  password: {
    value: '',
    focus: false,
    error: false
  },
  error: ""
}

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

  handleChange(field, e) {
    let error = ((field === "email" && !isEmail(e.target.value)) || (field === "password" && !e.target.value));
    this.setState({ [field]: { value: e.target.value, focus: this.state[field].focus, error: error } });
  }

  login(event) {
    event.preventDefault();

    const user = {
      mail: this.state.email.value,
      password: this.state.password.value
    };

    auth.login(user)
    .then(response => {
      auth.setAuthorizedUser(user);
      this.setState({error: ""})
      this.props.history.replace('/admin/resultados');
    })
    .catch(error => {
      this.setState({error: "Hubo un error, por favor vuelve a intentarlo"})
    })
  }

  formValidation() {
    return isEmail(this.state.email.value) && this.state.password.value
  }

  getClassName(field) {
    return (this.state[field] && this.state[field].error) ? ' error' : '';
  }

  render() {
    return (
      <div>
        <div className="login-container">
          <div className="Logo">
            <img src={logo} alt="Techy por el día" />
          </div>

          <div className="login-user">
            <form onSubmit={this.login}>
              <FormGroup
                controlId="formBasicText"
                bsSize="large"
                className={this.getClassName('email')}>
                <FormControl
                  type="email"
                  placeholder="Username"
                  value={this.state.email.value}
                  onChange={e => this.handleChange("email", e)}
                  onFocus={() => this.setState({email: {focus: true, value: this.state.email.value, error: this.state.email.error}}) }
                  onBlur={() => this.setState({email: {focus: false, value: this.state.email.value, error: this.state.email.error}}) }
                />
                <span className="input-error-message">Invalid Email</span>
              </FormGroup>
              <FormGroup bsSize="large" className={this.getClassName('password')}>
                <FormControl
                  type="password"
                  placeholder="Password"
                  value={this.state.password.value}
                  onChange={e => this.handleChange("password", e)}
                  onFocus={() => this.setState({password: {focus: true, value: this.state.password.value, error: this.state.email.error}}) }
                  onBlur={() => this.setState({password: {focus: false, value: this.state.password.value, error: this.state.email.error}}) }
                />
                <span className="input-error-message">Invalid Password</span>
              </FormGroup>
              <Button
                type="submit"
                className="btn-admin"
                bsSize="large" block
                disabled={!this.formValidation()}
              >Sign in</Button>
              <span className="error-message">{this.state.error}</span>
            </form>
          </div>
        </div>
      </div>
    );
  }
}

export {LoginAdminPage};
