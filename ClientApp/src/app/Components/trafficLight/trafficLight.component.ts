import { Component, OnInit } from '@angular/core';
import { delay } from 'rxjs';
import { TrafficLightModel } from 'src/app/Models/TrafficLightModel';
import { TrafficLightService } from 'src/app/Services/TrafficLight.service';

@Component({
  selector: 'app-trafficLight',
  templateUrl: './trafficLight.component.html',
  styleUrls: ['./trafficLight.component.css']
})
export class TrafficLightComponent implements OnInit {
  trafficLightConfiguration = {} as TrafficLightModel;
  time: number = 0;
  display:any ;
  interval:any;
  stopTrafficLight: boolean = false;
  pedestrianRequest: boolean = false

  redActive: boolean;
  yellowActive: boolean;
  greenActive: boolean;

  greenCounter:number;
  pedestrianRequestTrasitionCounter:number = 0;

  isTrafficLightStarted:boolean = false;

  constructor(private trafficLightService: TrafficLightService) { }

  ngOnInit() {
    this.trafficLightService.getTrafficLightConfiguration().subscribe((value) => {
      this.trafficLightConfiguration = value;
    });
  }

  StartTimer(){
    console.log("=====>");
    this.greenCounter = this.trafficLightConfiguration.greenLightTime;
    this.interval = setInterval(() => {
      if (this.time === 0) {
        this.time++;
      } else {
        this.time++;
      }
      if(this.pedestrianRequest == true) this.pedestrianRequestTrasitionCounter++;

      if(this.pedestrianRequest == false || 
        (this.pedestrianRequest == true && (this.time < (this.trafficLightConfiguration.redLightTime + this.trafficLightConfiguration.greenLightTime))) ||
        (this.pedestrianRequest == true && (this.time > (this.trafficLightConfiguration.redLightTime + (this.trafficLightConfiguration.greenLightTimeMax - 3)))))
      {
       this.trafficLightCycle();
      }
      else 
      {
        console.log("ushe mali ke vlezit kaj sho trebit")
        //console.log("pedestrianRequestTrasitionCounter = "+this.pedestrianRequestTrasitionCounter)
        if(this.greenActive == true && this.pedestrianRequestTrasitionCounter >= 3

          // ((this.time - this.trafficLightConfiguration.pedestrianCrossingTime) > this.trafficLightConfiguration.redLightTime)
          // && ((this.greenCounter + this.trafficLightConfiguration.pedestrianCrossingTime) < this.trafficLightConfiguration.greenLightTimeMax)
          )
        {
          console.log("vleze kaj sho trebit")
          this.yellowActive = false;
          this.greenActive = false;
          this.redActive = true;
          this.time = 0
          this.pedestrianRequest = false;
          this.pedestrianRequestTrasitionCounter = 0;
          clearInterval(this.interval);
          this.StartTimer();

          // console.log("Time before = "+this.time)
          // this.time = this.time - this.trafficLightConfiguration.pedestrianCrossingTime;
          // console.log("Time after = "+this.time)
          // this.greenCounter = this.greenCounter + this.trafficLightConfiguration.greenLightTime;
          // this.pedestrianRequest = false
          // console.log("GreenCounter = "+this.greenCounter)
        }else
        {
         // this.pedestrianRequest = false;
        }
        this.pedestrianRequestTrasitionCounter++;
        console.log("pedestrianRequestCounter" + this.pedestrianRequestTrasitionCounter)
      }
      this.display=this.transform( this.time)
    }, 1000);

  }
  transform(value: number): string {
    const minutes: number = Math.floor(value / 60);
    return minutes + ':' + (value - minutes * 60);
  }
  
  PedestrianRequest(){
    console.log("Pedestrian want to coss the street")
    if(this.greenActive == true) this.pedestrianRequest = true
  }
  StopTrafficLigh() {
    console.log("Traffic light is frozen")
    clearInterval(this.interval);
  }
  StartStopButton(){
    this.isTrafficLightStarted == true ? this.StopTrafficLigh() : this.StartTimer();
    this.isTrafficLightStarted = !this.isTrafficLightStarted;
  }

  trafficLightCycle(){
    if(this.time > this.trafficLightConfiguration.redLightTime + this.trafficLightConfiguration.greenLightTimeMax + this.trafficLightConfiguration.yellowLightTime  )
    {
      this.yellowActive = false;
      this.redActive = true;
      clearInterval(this.interval);
      this.time = 0
      this.StartTimer();
    }

    if(this.time <= this.trafficLightConfiguration.redLightTime){
    this.redActive = true;    
    //console.log("Traffic light : red")
    }

    if(this.time > (this.trafficLightConfiguration.redLightTime - this.trafficLightConfiguration.yellowLightTime)
        && (this.time <= this.trafficLightConfiguration.redLightTime))
    {
      this.yellowActive =true;
      //console.log("Traffic light : yellow to green")
    }

    if((this.time > this.trafficLightConfiguration.redLightTime) 
      && (this.time <= (this.trafficLightConfiguration.redLightTime + this.trafficLightConfiguration.greenLightTimeMax)))
    {
      this.greenCounter--;
      this.redActive = false;
      this.yellowActive = false;
      this.greenActive = true;
      //console.log("Traffic light : green")
    }

    if(this.time > this.trafficLightConfiguration.redLightTime + this.trafficLightConfiguration.greenLightTimeMax )
    {
      this.greenActive = false;
      this.yellowActive = true;
      
      //console.log("Traffic light : yellow to Red")
    }
  }
}
