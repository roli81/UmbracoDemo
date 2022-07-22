import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MachineOverviewComponent } from './components/machine-overview/machine-overview.component';
import { MachineDetailComponent } from './components/machine-detail/machine-detail.component';
import { LoginComponent } from './components/login/login.component';
import { NavigationComponent } from './components/navigation/navigation.component';
import { HttpClientModule } from '@angular/common/http';
import { authInterceptorProviders } from './interceptors/auth-interceptor';
import { MapComponent } from './components/map/map.component';
import { ChartComponent } from './components/chart/chart.component';



@NgModule({
  declarations: [
    AppComponent,
    MachineOverviewComponent,
    MachineDetailComponent,
    LoginComponent,
    NavigationComponent,
    MapComponent,
    ChartComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [authInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
