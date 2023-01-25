import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { WatchList } from './models/watch-list';
import { WatchListService } from './services/watch-list.service';

@Component({
  selector: 'app-watch-list',
  templateUrl: './watch-list.component.html',
  styleUrls: ['./watch-list.component.scss']
})
export class WatchListComponent implements OnInit {

  watchListList: WatchList[] = [];

  constructor(private watchListService: WatchListService,
    private router: Router) { }

  ngOnInit(): void {
    console.log("davainiciale")
    this.getWatchListList();
  }


  getWatchListList() {
    this.watchListService.getWatchListList().subscribe(data => {
      this.watchListList = data
    }, error => {
      console.log(error);
    })
  }


  changeStatus(mediaId: number) {

    this.watchListService.changeStatus(mediaId).subscribe(data => {
      this.getWatchListList();
      this.router.navigate(['/watchList'])

    }, error => {
      console.log(error);
    })
  }

  removeFromList(mediaId: number) {

    this.watchListService.removeFromWatchList(mediaId).subscribe(data => {
      this.getWatchListList();
      this.router.navigate(['/watchList'])

    }, error => {
      console.log(error);
    })
  }

}
