import React, { Component } from 'react';
import './adabot.css';
import ada from '../../assets/img/ada.png';
import logo from '../../assets/img/logo-techy-bot.svg';

class AdabotPage extends Component {
  
  constructor(props) {
    super(props);
    this.start = this.start.bind(this);
  }

  start(e) {
    e.preventDefault();
    this.props.history.replace("/bot");
  }

  render() {
    return (
      <div className="bot-page">
        <div className="Logo logo-bot">
            <img src={logo} alt="Techy por el día" />
        </div>
        <div className="intro">
          <img src={ada} title="Ada" alt="Ada" />
          <h2>Hola!</h2>
          <span>Me llamo <strong>Ada</strong></span>,
          <p>Te invito a jugar conmigo y te haré algunas preguntas.</p>
        </div>
        <button type="button" className="primary-button" onClick={this.start}>Empezar</button>
        </div>
    );
  }
}

export {AdabotPage};
