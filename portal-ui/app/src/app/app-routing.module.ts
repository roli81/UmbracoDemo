import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { MachineDetailComponent } from './components/machine-detail/machine-detail.component';
import { MachineOverviewComponent } from './components/machine-overview/machine-overview.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'overview', component: MachineOverviewComponent },
  { path: 'machines/:machineId', component: MachineDetailComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
