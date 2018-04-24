import React, { Component } from 'react';
import { Chat } from 'botframework-webchat';

import './bot.css';

class BotPage extends Component {
  render() {
    return [
      <h1>Techy por el día en Onetree</h1>,
      <Chat directLine={{ secret: '0jbPWuaFzhM.cwA.Cmw.r9PpU71NHz-RZccJoyjhi-cIKHEWkI2ffs6n1-xlqFs' }} user={{ id: 'test', name: 'TechyGirlsBot' }}/>
    ];
  }
}

export {BotPage};
