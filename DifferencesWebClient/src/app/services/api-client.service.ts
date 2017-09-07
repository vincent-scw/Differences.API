import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import { Config } from '../config';
import { User } from '../models/user';
import { BrowserStorage } from './browser-storage.service';

@Injectable() export class ApiClientService {
  private headers: Headers;
  private options: RequestOptions;
  private userChanged: Observable<User>;

  constructor(private http: Http) {
    // this.headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
    // this.options = new RequestOptions({ headers: this.headers });
  }
}
