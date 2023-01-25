import { WatchListComponent } from './components/watch-list/watch-list.component';
import { MediaIndexComponent } from './components/media/media-index/media-index.component';

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: '', component: MediaIndexComponent},
  {path: 'watchList', component: WatchListComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
