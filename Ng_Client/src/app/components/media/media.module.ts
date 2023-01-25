import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MediaIndexComponent } from './media-index/media-index.component';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from 'src/app/app-routing.module';




@NgModule({
  declarations: [
    MediaIndexComponent
  ],
  imports: [
    CommonModule,
    FormsModule,

    AppRoutingModule,
  ],
  exports: [MediaIndexComponent]
})
export class MediaModule { }
