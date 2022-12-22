import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TrafficLightModel } from '../Models/TrafficLightModel';
import { ServiceResponse } from '../Models/ServiceResponse';

@Injectable({
  providedIn: 'root'
})
export class TrafficLightService {

constructor(private http: HttpClient) { }
  baseUrl = "https://localhost:7000/api/";

  getTrafficLightConfiguration() {
    return this.http.get<TrafficLightModel>(this.baseUrl + 'trafficlight');
  }
  updateTrafficLightConfiguration(model: TrafficLightModel) {
    return this.http.post<ServiceResponse<TrafficLightModel>>(
      this.baseUrl + 'trafficlight',
      model
    );
  }
}
