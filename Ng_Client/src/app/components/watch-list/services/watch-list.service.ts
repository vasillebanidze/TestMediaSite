import { WatchList } from './../models/watch-list';

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WatchListService {

  baseUrl = environment.apiUrl;


  constructor(private httpClient: HttpClient) { }


  getWatchListList() {
    return this.httpClient.get<WatchList[]>(this.baseUrl + "WatchList/1");
  }

  changeStatus(mediaId: number) {
    return this.httpClient.put<any>(this.baseUrl + "WatchList?UserId=1&mediaId=" + mediaId, "");
  }

  addToWatchList(mediaId: number) {

    return this.httpClient.post<any>(this.baseUrl + "WatchList?UserId=1&mediaId=" + mediaId, {
      userId: 1,
      mediaId: mediaId
    });
  }


  removeFromWatchList(mediaId: number) {
    console.log("aq var watch 1")
    return this.httpClient.delete<any>(this.baseUrl + "WatchList?UserId=1&mediaId=" + mediaId);
  }

}
