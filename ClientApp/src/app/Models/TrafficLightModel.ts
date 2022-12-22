export interface TrafficLightModel {
    trafficLightColor: Color;
    redLightTime: number;
    yellowLightTime: number;
    greenLightTime: number;
    pedestrianCrossingTime: number;
}
export enum Color {
    red,
    yellow,
    green
}