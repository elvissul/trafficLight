import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TrafficLightModel } from '../Models/TrafficLightModel';
import { LightColors, TrafficLightSignalRModel } from '../Models/TrafficLightSignalRModel';
import { ServiceResponse } from '../Models/ServiceResponse';
import * as signalR from '@microsoft/signalr'
@Injectable({
  providedIn: 'root'
})
export class TrafficLightService {

constructor(private http: HttpClient) { }
  baseUrl = "https://localhost:7000/";

  //api calls

  GetTrafficLightConfiguration() {
    return this.http.get<ServiceResponse<TrafficLightModel>>(this.baseUrl + 'api/trafficlight');
  }
  UpdateTrafficLightConfiguration(model: TrafficLightModel) {
    return this.http.post<ServiceResponse<TrafficLightModel>>(
      this.baseUrl + 'api/trafficlight',
      model
    );
  }
 
  //Hub Connection

  private hubConnection: signalR.HubConnection;
  public activeLights: LightColors[];

  public StartConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder().withUrl(this.baseUrl + "trafficLight").build();

    this.hubConnection.start()
      .then(() => console.log('Connection Started'))
      .catch(err => console.log('Error while starting the connection' + err))
  }

  public AddTransferDataListener = () => {
    this.hubConnection.on('transferData', (data) => {
      this.activeLights = data;
      console.log(data);
    });
  }

  public PedestrianRequest = () => {
    this.hubConnection.invoke('PedestrianRequestToCross');
  }

  public StopTrafficLight = () => {
    this.hubConnection.invoke('StopTrafficLight');
  }

  public ResumeTrafficLight = () => {
    this.hubConnection.invoke('ResumeTrafficLight');
  }
}
