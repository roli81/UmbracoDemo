import { Metric } from "../models/metric";

export interface IMachineResponse {
    displayName: string;
    description: string;
    lat: number;
    long: number;
    customer: string;
    serialNr: string;
    key: string;
    dbKey: string;
    metrics: Metric[] | undefined;
    imageUrl: string;
}