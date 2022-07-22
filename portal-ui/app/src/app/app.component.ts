import { Component } from '@angular/core';
import { TokenStorageService } from './services/token-storage.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'portal-ui';
  user = this.tokenStorageService.getUser();


  constructor(private tokenStorageService: TokenStorageService) {



  }


  logout() {
    debugger;
    this.tokenStorageService.signOut();
    this.user = this.tokenStorageService.getUser();
  }
}
