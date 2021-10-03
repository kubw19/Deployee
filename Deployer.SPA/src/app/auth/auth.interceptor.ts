import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { from, Observable, throwError } from "rxjs";
import { catchError, retry, switchMap } from "rxjs/operators";
import { environment } from "src/environments/environment";
import { AuthService } from "./auth.service";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {


  constructor(private authService: AuthService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    let requestClone = req.clone()

    return from(this.authService.isAuthenticated())
      .pipe(
        switchMap(authenticated => {
          if (authenticated) {
            requestClone = req.clone({
              headers: requestClone.headers.append("Authorization", this.authService.authorizationHeaderValue)
            });
          }

          return next.handle(requestClone).pipe(
            catchError((error: HttpErrorResponse) => {
              if (!environment.production) {
                console.log(error)
              }
              return throwError(error);
            })
          );
        })
      );
  }
}
