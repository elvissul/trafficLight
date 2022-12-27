import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { TrafficLightModel } from 'src/app/Models/TrafficLightModel';
import { TrafficLightService } from 'src/app/Services/TrafficLight.service';

@Component({
  selector: 'app-trafficLight',
  templateUrl: './trafficLight.component.html',
  styleUrls: ['./trafficLight.component.css']
})
export class TrafficLightComponent implements OnInit {
  trafficLightConfiguration = {} as TrafficLightModel;
  errors:string = "";
  time: number = 0;
  display:any ;
  interval:any;
  stopTrafficLight: boolean = false;
  pedestrianRequest: boolean = false

  redActive: boolean;
  yellowActive: boolean;
  greenActive: boolean;

  greenCounter:number;

  isTrafficLightStarted:boolean = false;
  isFirstTimeStarted:boolean = true

  constructor(public trafficLightService: TrafficLightService, private http:HttpClient) { }

  ngOnInit() {
    this.trafficLightService.GetTrafficLightConfiguration().subscribe((value) => {
      if(value.message != null && value.success == false)
      {
        this.errors = value.message;
        console.log(this.errors)
      }else if(value.data != null){
        this.trafficLightConfiguration = value.data;
      }else{
        this.errors = "Something get wrong please try again later";
        console.log(this.errors)
      }
    });
  }

  private StartTrafficLight = () => {
    this.http.get('https://localhost:7000/api/TrafficLight/start').subscribe(res =>
      console.log(res)
    )}
  
  ActiveLight(color:number){
    return this.trafficLightService.activeLights.includes(color);
  }

  PedestrianRequest(){
    this.pedestrianRequest = true
    this.trafficLightService.PedestrianRequest();
    console.log("Pedestrian want to coss the street")

  }
  StopTrafficLigh() {
    console.log("Traffic light is stoped")
    this.trafficLightService.StopTrafficLight();
    clearInterval(this.interval);
  }
  ResumeTrafficlight(){
    console.log("Traffic light is started again")
    this.trafficLightService.ResumeTrafficLight();
  }
  StartStopButton(){
    if(this.isFirstTimeStarted){
      console.log("trafficlights started for the first time")
      this.trafficLightService.StartConnection();
      this.trafficLightService.AddTransferDataListener();
      this.StartTrafficLight();
      this.isFirstTimeStarted = false
    }
    else
    {
      console.log("Resume")
      this.isTrafficLightStarted = !this.isTrafficLightStarted;
      if(this.isTrafficLightStarted == true) 
      {
        this.StopTrafficLigh()
      }
      else{ 
        this.ResumeTrafficlight();
      }
    }
    
  }
}
