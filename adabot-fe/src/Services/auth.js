
class Auth {

  constructor() {
    this.user = null;
  }

  setAuthorizedUser(user) {
    this.user = {
      email: user.email
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
    return Promise.resolve();
  }

}

let auth = new Auth();
export { auth };
