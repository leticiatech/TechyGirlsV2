import React, { Component } from 'react';
import finals from '../../../assets/img/resultados-finales.png';
import techy from '../../../assets/img/logo-techy-sm.png';
import './resultadosFinales.css';

class ResultadosFinalesAdminPage extends Component {

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
		fetch('http://techygirlsbot.azurewebsites.net/api/getgroups')
			.then(response => response.json())
			.then(data => {
        		data.sort(function(a,b){
        		return  b.totalScore - a.totalScore;
        	});
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
    const groupScore = this.state.data.map((item, i) => {
      let alternateClass = "";
      if(i == 0){
        alternateClass = "total-score odd winner";
      }
      else{
        const isOdd = i % 2 ? "even" : "odd";
        alternateClass = "total-score " + isOdd;
      }
      return(
        <div key={i} className={alternateClass}>
          <span>{i+1}</span>
          <div className="group-score">
            <span className="g-name">{item.name}</span>
            <span className="g-score">{item.totalScore}<b>pts.</b></span>
          </div>
      </div>
       )
    })

      return (
      <div className="admin-page">
      <button type="button" className="small-button" onClick={this.goBot}>Desempatar</button>
        <img src={finals} alt="Resultados Finales" title="Resultados Finales" />
        <h3>Creating <strong>Ada<em>bOT</em></strong></h3>
        <div className="res-wrapper">{groupScore}</div>
        <img id="logo-sm" src={techy} alt="Techy por el día en Onetree" title="Techy por el día en Onetree" /> 
      </div>
    );
  }
}

export {ResultadosFinalesAdminPage};
