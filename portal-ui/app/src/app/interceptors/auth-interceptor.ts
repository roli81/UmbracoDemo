import { HTTP_INTERCEPTORS, HttpEvent } from '@angular/common/http';
import { Injectable, Injector } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';

import { Observable } from 'rxjs';
import { TokenStorageService } from '../services/token-storage.service';
const TOKEN_HEADER_KEY = 'Authorization';       // for Spring Boot back-end
@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  usertoken: any;
  constructor(private injector: Injector, private token: TokenStorageService) {
    
  }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.token.getToken();
    let tokenizeReq = request.clone({
      setHeaders: {
        Authorization: 'Bearer ' + token
      }
    })
    return next.handle(tokenizeReq);
  }
}
export const authInterceptorProviders = [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
];


