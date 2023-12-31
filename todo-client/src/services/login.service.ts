import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiLogin, httpOptions } from '../app/models/commonVariable';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  constructor(private http: HttpClient) {}

  private apiUrl = ApiLogin;
  private httpOptions = httpOptions;

  login(): Observable<any> {
    return this.http.get(`${this.apiUrl}/login`, httpOptions);
  }
}
