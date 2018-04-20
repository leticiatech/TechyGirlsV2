import React, { Component } from 'react';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import { ProtectedRoute } from './Components/ProtectedRoute';

import { BotPage } from './Pages/Bot/bot';
import { LoginAdminPage } from './Pages/Admin/Login/login';
import { ResultadosAdminPage } from './Pages/Admin/Resultados/resultados';

import './App.css';

class App extends Component {
  render() {
    return (
      <Router>
        <div>
          <Route exact path="/" component={BotPage} />
          <Route exact path="/admin" component={LoginAdminPage} />
          <ProtectedRoute path="/admin/resultados" component={ResultadosAdminPage} />
        </div>
      </Router>
    );
  }
}

export default App;
