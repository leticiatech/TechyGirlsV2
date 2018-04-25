import React, { Component } from 'react';
import partials from '../../../assets/img/resultados-parciales.png';
import techy from '../../../assets/img/logo-techy-sm.png';
import './resultados.css';

class ResultadosAdminPage extends Component {
  constructor(props){
    super(props);
    this.end = this.end.bind(this);
  }

  end(e) {
    e.preventDefault();
    this.props.history.replace('/admin/resultadosFinales');
  }

  render() {
      return (
      <div className="admin-page">
        <img src={partials} alt="Resultados Parciales" title="Resultados Parciales" />
        <h3>Creating <strong>Ada<em>bOT</em></strong></h3>
        <div className="group-row">
          <table>
            <tbody>
              <tr>
                <th className="team">Equipo</th>
                <th>P.1</th>
                <th>P.2</th>
                <th>P.3</th>
                <th>P.4</th>
                <th>P.5</th>
              </tr>
              <tr>
                <td rowSpan="3" className="group-name">March 8</td>
                <td><strong>5</strong>pts.</td>
                <td><strong>3</strong>pts.</td>
                <td><strong>3</strong>pts.</td>
                <td><strong>3</strong>pts.</td>
                <td><strong>3</strong>pts.</td>
              </tr>
              <tr>
                <th>P.6</th>
                <th>P.7</th>
                <th>P.8</th>
                <th>P.9</th>
                <th>P.10</th>
              </tr>
              <tr>
                <td><strong>0</strong>pts.</td>
                <td><strong>5</strong>pts.</td>
                <td><strong>5</strong>pts.</td>
                <td><strong>5</strong>pts.</td>
                <td><strong>0</strong>pts.</td>
              </tr>
              </tbody>
          </table>
        </div>
        <div className="group-row">
          <table>
            <tbody>
              <tr>
                <th className="team">Equipo</th>
                <th>P.1</th>
                <th>P.2</th>
                <th>P.3</th>
                <th>P.4</th>
                <th>P.5</th>
              </tr>
              <tr>
                <td rowSpan="3" className="group-name">March 8</td>
                <td><strong>5</strong>pts.</td>
                <td><strong>3</strong>pts.</td>
                <td><strong>3</strong>pts.</td>
                <td><strong>3</strong>pts.</td>
                <td><strong>3</strong>pts.</td>
              </tr>
              <tr>
                <th>P.6</th>
                <th>P.7</th>
                <th>P.8</th>
                <th>P.9</th>
                <th>P.10</th>
              </tr>
              <tr>
              <td><strong>0</strong>pts.</td>
                <td><strong>5</strong>pts.</td>
                <td><strong>5</strong>pts.</td>
                <td><strong>5</strong>pts.</td>
                <td><strong>0</strong>pts.</td>
              </tr>
              </tbody>
          </table>
        </div>
        <button type="button" className="secondary-button" onClick={this.end}>Finalizar juego</button>
        <img id="logo-sm" src={techy} alt="Techy por el día en Onetree" title="Techy por el día en Onetree" /> 
      </div>
    );
  }
}

export {ResultadosAdminPage};
