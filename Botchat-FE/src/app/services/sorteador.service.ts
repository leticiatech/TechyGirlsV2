import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

export interface Usuario {
  Nombre: string;
  Apellido: string;
  Telefono: string;
  Email: string;
  Id: number;
}


@Injectable()
export class SorteadorService {

	apis = {
		registro: 'http://techygirls.azurewebsites.net/Api/Sorteo/Registrar/',
		sortear: 'http://techygirls.azurewebsites.net/Api/Sorteo/Sortear',
		consulta: 'http://techygirls.azurewebsites.net/Api/Sorteo/ConsultarNro/',
		cerrar: 'http://techygirls.azurewebsites.net/Api/Sorteo/TotalParticipantes/',
	}

	headers = new Headers({ 'Content-Type': 'application/json' });
  options = new RequestOptions({ headers: this.headers });

  constructor(private http: Http) { }

  sortear(): Observable<any> {
  	return this.http.get(this.apis.sortear, this.options)
      .map(this.extractData)
      .catch(this.handleError);
  }

  registro(usuario: Usuario): Observable<any> {
    return this.http.post(this.apis.registro, usuario, this.options)
      .map(this.extractData)
      .catch(this.handleError);

  }

  consultaNro(email: string): Observable<any> {
  	return this.http.get(this.apis.consulta + '?email=' + email, this.options)
      .map(this.extractData)
      .catch(this.handleError);
  }

  cerrarSorteo(): Observable<any> {
  	return this.http.get(this.apis.cerrar, this.options)
      .map(this.extractData)
      .catch(this.handleError);
  }

  private extractData(res: Response) {
    console.log('res', res);
    let body = res.json();
    console.log('body', body)
    return  body.data || body || {};
  }

  private handleError(error: Response | any) {
    let errMsg: string;
    if (error instanceof Response) {
      const body = error.json() || '';
      const err = body.error || JSON.stringify(body);
      errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
    } else {
      errMsg = error.message ? error.message : error.toString();
    }
    console.error(errMsg);
    return Observable.throw(errMsg);

  }
}
