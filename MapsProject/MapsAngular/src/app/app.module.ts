import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AgmCoreModule } from '@agm/core';
import { HttpClientModule } from '@angular/common/http';

//Plugins
import {Select2Component} from 'angular-select2-component';


import { AppComponent } from './app.component';
import { MapComponent } from './map/map.component';


@NgModule({
  
  declarations: [
    AppComponent,
    MapComponent//,
    //Select2Component
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyCwOwKLOzZWKoDnC4iFERxfaOQ5BodAMDU'
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
