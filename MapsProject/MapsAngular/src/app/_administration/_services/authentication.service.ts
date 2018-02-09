import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { HttpClient, HttpHeaders } from '@angular/common/http';
// tslint:disable-next-line:import-blacklist
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { Token } from '../_models/token';

@Injectable()
export class AuthenticationService {
  public token: string;

  constructor(private http: HttpClient) {
    // set token if saved in local storage
    const currentUser = JSON.parse(localStorage.getItem('currentUser'));
    this.token = currentUser && currentUser.token;
  }

  isAuthenticated(): boolean {
    if (localStorage.getItem('currentUser')) {
      // logged in so return true
      return true;
    } else {
      return false;
    }
  }

  login(username: string, password: string): Observable<boolean> {
    const body = { username: username, password: password, grant_type: 'password' };
    let p = [];
    // tslint:disable-next-line:forin
    for (const key in body) {
      p.push(key + '=' + encodeURIComponent(body[key]));
    }
    p = p.join('&');
    const headers = new HttpHeaders({
      'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
    });
    // Content-Type
    return this.http.post('http://localhost:50109/token', p, { headers: headers })
      .map((response: Token) => {
        // login successful if there's a jwt token in the response
        const token = response.access_token;
        if (token) {
          // set token property
          this.token = token;

          // store username and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUser', JSON.stringify({ username: username, token: token }));

          // return true to indicate successful login
          return true;
        } else {
          // return false to indicate failed login
          return false;
        }
      });
  }

  logout(): void {
    // clear token remove user from local storage to log user out
    this.token = null;
    localStorage.removeItem('currentUser');
  }
}
