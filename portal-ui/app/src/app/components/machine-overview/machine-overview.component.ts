import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Machine } from 'src/app/models/machine';
import { MachineService } from 'src/app/services/machine.service';

@Component({
  selector: 'app-machine-overview',
  templateUrl: './machine-overview.component.html',
  styleUrls: ['./machine-overview.component.scss']
})
export class MachineOverviewComponent implements OnInit {


  machines: Observable<Machine[]> = this._machineService.getAll();

  constructor(private _machineService: MachineService) { }

  ngOnInit(): void {
  }

}
