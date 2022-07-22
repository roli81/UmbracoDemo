import { Metric } from "./metric";

export class Machine {
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
        
    constructor(
        displayName: string, 
        description: string, 
        lat: number, 
        long: number, 
        customer: string, 
        serialNr: string, 
        key: string, 
        dbKey: string,
        imageUrl: string) {
            this.displayName = displayName;
            this.description = description;
            this.lat = lat;
            this.long = long;
            this.customer = customer;
            this.serialNr = serialNr;
            this.key = key;
            this.dbKey = dbKey;
            this.imageUrl = imageUrl;
    }
}