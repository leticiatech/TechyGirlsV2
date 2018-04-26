import React, { Component } from 'react';
import partials from '../../../assets/img/resultados-parciales.png';
import techy from '../../../assets/img/logo-techy-sm.png';
import './resultados.css';

class ResultadosAdminPage extends Component {
  constructor(props){
    super(props);
    this.end = this.end.bind(this);
    this.state = {
      data:[]
    };
  }

  end(e) {
    e.preventDefault();
    this.props.history.replace('/admin/resultadosFinales');
  }

  loadScores() {
		fetch('http://techygirlsbot.azurewebsites.net/api/getgroups')
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
    const groupPoints = this.state.data.map((item, i) => {
        let questionScores = new Array(10);
        item.questionScores.forEach(element => {
          questionScores[parseInt(element.question, 10)-1] = element.score;
        });
      return( 
        <div key={i} className="group-row">
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
                <td key={i} rowSpan="3" className="group-name">{item.name}</td>
                <td className="points"><strong>{questionScores[0]}</strong>pts.</td>
                <td className="points"><strong>{questionScores[1]}</strong>pts.</td>
                <td className="points"><strong>{questionScores[2]}</strong>pts.</td>
                <td className="points"><strong>{questionScores[3]}</strong>pts.</td>
                <td className="points"><strong>{questionScores[4]}</strong>pts.</td>
              </tr>
              <tr>
                <th>P.6</th>
                <th>P.7</th>
                <th>P.8</th>
                <th>P.9</th>
                <th>P.10</th>
              </tr>
              <tr>
                <td className="points"><strong>{questionScores[5]}</strong>pts.</td>
                <td className="points"><strong>{questionScores[6]}</strong>pts.</td>
                <td className="points"><strong>{questionScores[7]}</strong>pts.</td>
                <td className="points"><strong>{questionScores[8]}</strong>pts.</td>
                <td className="points"><strong>{questionScores[9]}</strong>pts.</td>
              </tr>
              </tbody>
          </table>
        </div>
      );
    })
  
      return (
      <div className="admin-page">
        <img src={partials} alt="Resultados Parciales" title="Resultados Parciales" />
        <h3>Creating <strong>Ada<em>bOT</em></strong></h3>
        {groupPoints}
        <button type="button" className="secondary-button" onClick={this.end}>Finalizar juego</button>
        <img id="logo-sm" src={techy} alt="Techy por el día en Onetree" title="Techy por el día en Onetree" /> 
      </div>
    );
  }
}

export {ResultadosAdminPage};
