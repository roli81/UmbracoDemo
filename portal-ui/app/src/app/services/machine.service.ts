import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Machine } from '../models/machine';
import { environment } from '../../environments/environment'
import { IMachineResponse } from '../interfaces/IMachineResponse';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MachineService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<Machine[]> {
    return this.http.get<IMachineResponse[]>(`https://localhost:44354/api/machines`)
      .pipe(
        map(response => {
          return response.map(item => new Machine(
            item.displayName,
            item.description,
            item.lat,
            item.long,
            item.customer,
            item.serialNr,
            item.key,
            item.dbKey,
            item.imageUrl
          ));
        }
        ));
  }


  getById(machineId: string): Observable<Machine> {
    return this.http.get<IMachineResponse>(`https://localhost:44354/api/machines/${machineId}`)
      .pipe(
        map(item => {
          return new Machine(
            item.displayName,
            item.description,
            item.lat,
            item.long,
            item.customer,
            item.serialNr,
            item.key,
            item.dbKey,
            item.imageUrl
          );
        })
      )

  }

}
