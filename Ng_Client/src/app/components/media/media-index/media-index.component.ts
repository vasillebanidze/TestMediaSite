import { MediaService } from './../Services/media.service';
import { Component, OnInit } from '@angular/core';
import { Media } from '../models/media';
import { WatchListService } from '../../watch-list/services/watch-list.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-media-index',
  templateUrl: './media-index.component.html',
  styleUrls: ['./media-index.component.scss']
})
export class MediaIndexComponent implements OnInit {

  searchInput: string = "";
  mediaTypeId: number = 0;
  mediaList: Media[] = [];


  constructor(private mediaService: MediaService, private watchListService: WatchListService,
    private router: Router) { }

  ngOnInit(): void {

    this.getMediaList();
  }


getMediaList(){
  this.mediaService.getMediaList(this.mediaTypeId, this.searchInput).subscribe(data => {
    this.mediaList = data
  }, error => {
    console.log(error);
  })
}


  setFilter(id: number)
  {

      this.mediaTypeId = id;
      this.getMediaList();

  }

  clearSearch(){
    this.searchInput = "";
    this.getMediaList();
  }

  addTowatchList(mediaId: number){
   this.watchListService.addToWatchList(mediaId);
   this.watchListService.getWatchListList();
   this.router.navigate(['/watchList'])
  }
}
