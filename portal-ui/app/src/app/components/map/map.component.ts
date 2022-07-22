/// <reference types="@types/googlemaps" />
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MachineLocation } from 'src/app/models/machineLocation';
import { MachineService } from 'src/app/services/machine.service';
declare let google: any;


@Component({
  selector: 'app-map',
  template: `
      <div id="map"></div>      
  `,
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit {




  constructor(private machineService: MachineService, private router: Router) {
  }

  ngOnInit(): void {
    this.machineService.getAll().subscribe(machines => {
      debugger;
      let machinePoints: Array<MachineLocation> = [];
      machines.forEach(m => {
        const existingMachinePoint = machinePoints.find(mp => mp.lat === m.lat && mp.long == m.long);

        if (existingMachinePoint) {
          existingMachinePoint.text += `<br /><strong>${m.serialNr}</strong><br /> ${m.description}<button  class="detail-btn"  data-machineId="${m.key}">Machine Details</button>`;
        } else {
          machinePoints.push({
            latLong: new google.maps.LatLng(m.lat, m.long),
            text: `<strong>${m.serialNr}</strong><br /> ${m.description}<button class="detail-btn" data-machineId="${m.key}">Machine Details</button>`,
            city: '',
            lat: m.lat,
            long: m.long,
            machineKey: m.key,
            machineName: m.displayName, 
            hasError: false
          });
        }
      });

      
      this.initMap(machinePoints);
    });

  }


  initMap(points: Array<MachineLocation>) : void {
  /// get map div with angular
  const map = new google.maps.Map(
    document.getElementById("map") as HTMLElement,
    {
      center: points[0].latLong,
      zoom: 10,
    }
  );

  points.forEach(p => {
    const infowindow = new google.maps.InfoWindow({
      content: this.createInfoWindowContent(p, map.getZoom()!),
    });

    google.maps.event.addListener(infowindow, 'domready', () => {
      const elements = document.getElementsByClassName('detail-btn');

      for (let i = 0; i < elements.length; i++) {
        elements[i].addEventListener('click', (event : any) => {
          const machineId : string = event?.target?.getAttribute('data-machineId');
          this.router.navigate([`/machines/${machineId}`]);
        });
      }
    });

    const marker = new google.maps.Marker({
      position: p.latLong,
      map,
      title: p.machineName
    });

    marker.addListener('click', () => {
      infowindow.open({
        anchor: marker,
        map,
        shouldFocus: false,
      });
    });
  });
  }


  createInfoWindowContent(point: MachineLocation, zoom: number) {
    let result = `
    <style>
      button {
        background-color: #282828;
        display: block;
        border: 0;
        margin-top: 1em;
        padding: 0.5em;
        font-weight: bold;
        color: #ffffff;
        border-radius: 4px;
        cursor: pointer;
      }
  
      button:hover {
        color: #99CC33;
      }
    </style> 
    `;
    result += `${[
      point.city,
      point.text
    ].join("<br />")}`;


    return result;
  }

}







