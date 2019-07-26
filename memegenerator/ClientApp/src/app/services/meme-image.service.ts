import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MemeImageService {

  constructor(private http: HttpClient) { }

  private memesUrl = '/MemeImage/Get';

  getMemes() {
    return this.http.get(this.memesUrl);
  }
}
