import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TrafficLightComponent } from "./Components/trafficLight/trafficLight.component";
import { TrafficLightConfigurationComponent } from './Components/trafficLightConfiguration/trafficLightConfiguration.component';
import { TrafficLightService } from './Services/TrafficLight.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
    declarations: [
        AppComponent,
        TrafficLightComponent,
        TrafficLightConfigurationComponent,
    ],
    providers: [TrafficLightService],
    bootstrap: [AppComponent],
    imports: [
        BrowserModule,
        AppRoutingModule,
        NgbModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule
    ]
})
export class AppModule { }
