import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MemeImageService {

  private memesUrl = '/api/memes';

  constructor(private http: HttpClient) { }

  getMeme(id: number) {
    return this.http.get(this.memesUrl + '/' + id);
  }
}
