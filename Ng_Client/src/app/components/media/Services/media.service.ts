import { Media } from '../models/media';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MediaService {

  baseUrl = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }


  getMediaList(mediaTypeId: number, searchTerm: string) {
    if (mediaTypeId) {

      if (searchTerm) {
        return this.httpClient.get<Media[]>(this.baseUrl + "Media?mediaTypeId=" + mediaTypeId + "&searchTerm=" + searchTerm);
      }
      return this.httpClient.get<Media[]>(this.baseUrl + "Media?mediaTypeId=" + mediaTypeId);
    }
    if (searchTerm) {
      return this.httpClient.get<Media[]>(this.baseUrl + "Media?searchTerm=" + searchTerm);
    }

    return this.httpClient.get<Media[]>(this.baseUrl + "Media");

  }



}
