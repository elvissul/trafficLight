/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { TrafficLightService } from './TrafficLight.service';

describe('Service: TrafficLight', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TrafficLightService]
    });
  });

  it('should ...', inject([TrafficLightService], (service: TrafficLightService) => {
    expect(service).toBeTruthy();
  }));
});
