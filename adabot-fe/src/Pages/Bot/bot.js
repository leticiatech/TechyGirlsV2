import React, { Component } from 'react';
import { Chat } from 'botframework-webchat';

import './bot.css';

class BotPage extends Component {
  render() {
    return (
      <Chat directLine={{ secret: '0jbPWuaFzhM.cwA.Cmw.r9PpU71NHz-RZccJoyjhi-cIKHEWkI2ffs6n1-xlqFs' }} user={{ id: 'test', name: 'TechyGirlsBot' }}/>
    );
  }
}

export {BotPage};
