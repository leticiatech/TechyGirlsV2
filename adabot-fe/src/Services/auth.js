import * as axios from 'axios';
import axiosRetry from 'axios-retry';
import * as config from './config';

class Auth {

  constructor() {
    this.user = null;

    this.instance = axios.create({
        baseURL: config.API_PATH,
        headers: {
          'Content-Type': 'application/json; charset=UTF-8',
          'Medable-Client-Key': config.MEDABLE_CLIENT_KEY
        }
    });
    axiosRetry(this.instance, { retries: 2, retryDelay: (retryCount) => 500 });
  }

  setAuthorizedUser(user) {
    this.user = {
      mail: user.mail
    };
    window.localStorage.setItem('authenticated', true);
    window.localStorage.setItem('authenticated-user', JSON.stringify(this.user));
  }

  logoutAuthorizedUser() {
    window.localStorage.setItem('authenticated', false);
    window.localStorage.setItem('authenticated-user', null);
  }

  getAuthorizedUser() {
    const authenticated = window.localStorage.getItem('authenticated');
    if (authenticated && authenticated === 'true') {
        return this.user ||
          JSON.parse(window.localStorage.getItem('authenticated-user'));
    }
    return null;
  }

  login(user) {
    console.log(user)
    return this.instance.post(config.AUTH_API, user);
  }

}

let auth = new Auth();
export { auth };
