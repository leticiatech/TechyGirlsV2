import React, { Component } from 'react';
import { Chat } from 'botframework-webchat';
import logo from '../../assets/img/logo-techy-bot.svg';

import './bot.css';

class BotPage extends Component {
  
constructor(props) {
  super(props);
  this.goResults = this.goResults.bind(this);
  this.goUntie = this.goUntie.bind(this);
  this.refresh = this.refresh.bind(this);
}

goResults(e) {
  e.preventDefault();
  this.props.history.replace("/admin/login");
}
goUntie(e) {
  e.preventDefault();
  this.props.history.replace("/admin/desempate");
}
refresh(e){ 
  e.preventDefault();
  console.log("hago refresh");
  window.location.reload(); 
}

  render() {
    return [
      <button type="button" className="small-button" onClick={this.goResults}>Resultados</button>,
      <button type="button" className="small-button seeUntie" onClick={this.goUntie}>Desempate</button>,
      <div className="Logo logo-bot">
            <img src={logo} alt="Techy por el día" />
        </div>,
        <button className="refresh-button" onClick={this.refresh}>Desempatar</button>,
      <Chat directLine={{ secret: '0jbPWuaFzhM.cwA.Cmw.r9PpU71NHz-RZccJoyjhi-cIKHEWkI2ffs6n1-xlqFs' }} user={{ id: 'test', name: 'TechyGirlsBot' }}/>
    ];
  }
}

export {BotPage};
