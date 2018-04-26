import React, { Component } from 'react';
import finals from '../../../assets/img/resultados-finales.png';
import techy from '../../../assets/img/logo-techy-sm.png';
import './resultadosFinales.css';

class DesempateAdminPage extends Component {

  constructor(props){
    super(props);
    this.goBot = this.goBot.bind(this);
    this.state = {
      data:[]
    };
  }

  goBot(e) {
    e.preventDefault();
    this.props.history.replace('/bot');
  }

  loadScores() {
		fetch('http://techygirlsbot.azurewebsites.net/api/getwinner')
			.then(response => response.json())
			.then(data => {        		
			  this.setState({data: data })
		})
			.catch(err => {
        this.setState({error: "Hubo un error, por favor vuelve a intentarlo"})
      })
  }
  
  componentDidMount() {
    this.loadScores()
  }  

  render() {
      return (
      <div className="admin-page">
        <img src={finals} alt="Resultados Finales" title="Resultados Finales" />
        <h3>Creating <strong>Ada<em>bOT</em></strong></h3>
        <div className="res-wrapper">
        <div className="total-score odd winner">
          <span>1</span>
          <div className="group-score">
            <span className="g-name">{this.state.data.name}</span>
            <span className="g-score">{this.state.data.totalScore}<b>pts.</b></span>
          </div>
      </div>
        </div>
        <img id="logo-sm" src={techy} alt="Techy por el día en Onetree" title="Techy por el día en Onetree" /> 
      </div>
    );
  }
}

export {DesempateAdminPage};
