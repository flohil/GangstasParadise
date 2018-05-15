import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import {
  MatButtonModule, MatCardModule, MatDividerModule, MatFormField, MatFormFieldModule, MatIconModule, MatInputModule,
  MatProgressSpinnerModule,
  MatToolbarModule
} from '@angular/material';
import {TextGeneratorService} from './text-generator.service';
import {HttpClientModule} from '@angular/common/http';
import {MusicGeneratorService} from './music-generator.service';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';



@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MatCardModule,
    MatToolbarModule,
    MatButtonModule,
    MatDividerModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatProgressSpinnerModule,
    HttpClientModule
  ],
  providers: [TextGeneratorService, MusicGeneratorService],
  bootstrap: [AppComponent]
})
export class AppModule { }
