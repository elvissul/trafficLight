import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { TrafficLightModel } from 'src/app/Models/TrafficLightModel';
import { TrafficLightService } from 'src/app/Services/TrafficLight.service';

@Component({
  selector: 'app-trafficLightConfiguration',
  templateUrl: './trafficLightConfiguration.component.html',
  styleUrls: ['./trafficLightConfiguration.component.css']
})
export class TrafficLightConfigurationComponent implements OnInit {
  @ViewChild('editForm', { static: true }) editForm: NgForm;
  @HostListener('window:beforeunload', ['$event']) unloadNotification(
    $event: any
  ) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }
  trafficLightConfiguration = {} as TrafficLightModel;
  errors:string = "";

  constructor(private trafficLightService: TrafficLightService) {

   }
  
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
  editTrafficLightConfiguration()
  {
    this.trafficLightService.UpdateTrafficLightConfiguration(this.trafficLightConfiguration).subscribe((value) => {
      if(value.message != null && value.success == false)
      {
        this.errors = value.message;
      }else if(value.data != null){
        this.trafficLightConfiguration = value.data;
      }else{
        this.errors = "Something get wrong please try again later";
      }
    });
  }

}
