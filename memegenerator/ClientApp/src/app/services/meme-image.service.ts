import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { MemeImage } from '../models/meme-image';

@Injectable({
  providedIn: 'root'
})
export class MemeImageService {

  private memesUrl = '/api/memes';

  constructor(private http: HttpClient) { }

  public getMemes(page: number, pageSize: number) {
    return this.http.get<MemeImage[]>(this.memesUrl + "/" + page + "/" + pageSize);
  }
}
