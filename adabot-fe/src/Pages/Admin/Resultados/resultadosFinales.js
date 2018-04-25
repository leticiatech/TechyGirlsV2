import React, { Component } from 'react';
import finals from '../../../assets/img/resultados-finales.png';
import techy from '../../../assets/img/logo-techy-sm.png';
import './resultadosFinales.css';

class ResultadosFinalesAdminPage extends Component {

  render() {
      return (
      <div className="admin-page">
        <img src={finals} alt="Resultados Finales" title="Resultados Finales" />
        <h3>Creating <strong>Ada<em>bOT</em></strong></h3>
        <div className="res-wrapper">
          <div className="total-score odd winner">
            <span>1</span>
            <div className="group-score">
              <span className="g-name">WeGirls!</span>
              <span className="g-score">17<b>pts.</b></span>
            </div>
          </div>
          <div className="total-score even">
            <span>2</span>
              <div className="group-score">
                <span className="g-name">GoGirls!</span>
                <span className="g-score">13<b>pts.</b></span>
              </div>
          </div>
          <div className="total-score odd">
            <span>3</span>
            <div className="group-score">
              <span className="g-name">WeGirls!</span>
              <span className="g-score">10<b>pts.</b></span>
            </div>
          </div>
        </div>
        <img id="logo-sm" src={techy} alt="Techy por el día en Onetree" title="Techy por el día en Onetree" /> 
      </div>
    );
  }
}

export {ResultadosFinalesAdminPage};
