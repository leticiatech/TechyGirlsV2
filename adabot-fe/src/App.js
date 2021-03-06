import React, { Component } from 'react';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import { ProtectedRoute } from './Components/ProtectedRoute';

import { AdabotPage } from './Pages/Adabot/adabot';
import { BotPage } from './Pages/Bot/bot';
import { AdminPage } from './Pages/Admin/Admin/admin';
import { LoginAdminPage } from './Pages/Admin/Login/login';
import { ResultadosAdminPage } from './Pages/Admin/Resultados/resultados';
import { ResultadosFinalesAdminPage } from './Pages/Admin/Resultados/resultadosFinales';
import { DesempateAdminPage } from './Pages/Admin/Resultados/desempate';

import './app.css';

class App extends Component {
  render() {
    return (
      <Router>
        <div>
          <Route exact path="/" component={AdabotPage} />
          <Route exact path="/bot" component={BotPage} />
          <Route exact path="/admin" component={AdminPage} />
          <Route exact path="/admin/login" component={LoginAdminPage} />
          <ProtectedRoute path="/admin/resultados" component={ResultadosAdminPage} />
          <ProtectedRoute path="/admin/resultadosFinales" component={ResultadosFinalesAdminPage} />
          <ProtectedRoute path="/admin/desempate" component={DesempateAdminPage} />
        </div>
      </Router>
    );
  }
}

export default App;
