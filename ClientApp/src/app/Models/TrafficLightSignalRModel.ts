export interface TrafficLightSignalRModel {
    activeLights: LightColors[];
    secondsUntilNextChange: number;
}
export enum LightColors {
    Red,
    Yellow,
    Green
}