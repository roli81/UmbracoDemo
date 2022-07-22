import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import * as echarts from 'echarts';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.scss']
})
export class ChartComponent implements AfterViewInit {


  @ViewChild("container") containerDiv!: ElementRef;
  
  constructor() { }
  ngAfterViewInit(): void {
    this.drawChart();
  }





  drawChart() {
          // Initialize the echarts instance based on the prepared dom
          var myChart = echarts.init(this.containerDiv.nativeElement);

          // Specify the configuration items and data for the chart
          var option = {
            title: {
              text: 'ECharts Getting Started Example'
            },
            tooltip: {},
            legend: {
              data: ['sales']
            },
            xAxis: {
              data: ['Shirts', 'Cardigans', 'Chiffons', 'Pants', 'Heels', 'Socks']
            },
            yAxis: {},
            series: [
              {
                name: 'sales',
                type: 'bar',
                data: [5, 20, 36, 10, 10, 20]
              }
            ]
          };
    
          // Display the chart using the configuration items and data just specified.
          myChart.setOption(option);
  }
}
