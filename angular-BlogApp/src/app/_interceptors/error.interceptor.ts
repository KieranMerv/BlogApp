import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private toastr: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error => {
        if (error) {
          switch (error.status) {
            case 400:
              if (error.error.errors) {
                const modelStateErrors = [];
                for (const key in error.error.errors) {
                  if (error.error.errors) {
                    modelStateErrors.push(error.error.errors[key])
                  }
                }
                throw modelStateErrors.flat(Infinity);
              } else if (typeof(error.error) === "object") {
                this.toastr.error("Bad Request.", error.status);
              } else {
                this.toastr.error(error.error, error.status);
              }
              break;
            case 401:
              this.toastr.error("Unauthorized. Check credentials and password.", error.status);
              break;
            case 404:
              this.toastr.error("Not found.", error.status);
              break;
            case 500:
              this.toastr.error("Server Error.", error.status);
              break;
            default:
              this.toastr.error("Something went wrong.", error.status);
              console.log(error);
              break;
          }
        }
        return throwError(() => error);
      })
    );
  }
}
