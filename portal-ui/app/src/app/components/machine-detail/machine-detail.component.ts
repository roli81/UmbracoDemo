import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { filter, map, mergeMap } from 'rxjs/operators';
import { Machine } from 'src/app/models/machine';
import { MachineService } from 'src/app/services/machine.service';

@Component({
  selector: 'app-machine-detail',
  templateUrl: './machine-detail.component.html',
  styleUrls: ['./machine-detail.component.scss']
})
export class MachineDetailComponent implements OnInit {


  machine$: Observable<Machine> | undefined;

  constructor(private _machineService: MachineService, 
    private route: ActivatedRoute) { }

  ngOnInit(): void {


    var machineIdObservable = this.route.paramMap.pipe(
      map(params => params.get('machineId')),
      filter(this.isNonNull)
    );


    this.machine$ = machineIdObservable
      .pipe(
        mergeMap(machineId => this._machineService.getById(machineId))
      );


  }


  isNonNull<T>(value: T): value is NonNullable<T> {
    return value !== null;
  }
}
