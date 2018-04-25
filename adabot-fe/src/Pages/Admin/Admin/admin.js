import React, { Component } from 'react';

import './admin.css';
import logo from '../../../assets/img/logo-techy.svg';
import { Button, Row } from 'react-bootstrap';

class AdminPage extends Component {

  constructor(props){
    super(props);
  }

  gotoLogin() {
    this.props.history.replace('/admin/login');
  }

  render() {
    return (
      <div className="res-page">
        <Row>
          <img src={logo} title="Techy por el día" alt="Techy por el día" />
        </Row>
        <Row className="ver-resultados">
          <Button className="btn-admin" onClick={() => this.gotoLogin()} >VER RESULTADOS</Button>
        </Row>
      </div>
    );
  }
}

export {AdminPage};
