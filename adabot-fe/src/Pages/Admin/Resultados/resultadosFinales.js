import React, { Component } from 'react';
import finals from '../../../assets/img/resultados-finales.png';
import techy from '../../../assets/img/logo-techy-sm.png';
import './resultadosFinales.css';

class ResultadosFinalesAdminPage extends Component {

  constructor(props){
    super(props);
    this.state = {
      data:[]
    };
  }

  loadData() {
		fetch('http://techygirlsbot.azurewebsites.net/api/getgroups')
			.then(response => response.json())
			.then(data => {
        		data.sort(function(a,b){
        		return  b.totalScore - a.totalScore;
        	});
			this.setState({data: data })
		})
			.catch(err => console.error(this.props.url, err.toString()))
  }
  
  componentDidMount() {
    this.loadData()
  }  

  render() {
    const groupScore = this.state.data.map((item, i) => {
      let myClassName = "";
      if(i == 0){
        myClassName = "total-score odd winner";
      }
      else{
        const isOdd = i % 2 ? "even" : "odd";
        myClassName = "total-score " + isOdd;
      }
      return(
        <div key={i} className={myClassName}>
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
        <img src={finals} alt="Resultados Finales" title="Resultados Finales" />
        <h3>Creating <strong>Ada<em>bOT</em></strong></h3>
        <div className="res-wrapper">
        	{groupScore}
        </div>
        <img id="logo-sm" src={techy} alt="Techy por el día en Onetree" title="Techy por el día en Onetree" /> 
      </div>
    );
  }
}

export {ResultadosFinalesAdminPage};
