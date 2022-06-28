import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, Observable } from 'rxjs';
import { UsersApiCallsService } from '../_services/users-api-calls.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private usersApiCallsService: UsersApiCallsService,
    private router: Router,
    private toastr: ToastrService) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.usersApiCallsService.currentUser$.pipe(
      map(user => {
        if (user) return true;
        this.toastr.error("Unauthorized page access detected.");
        console.log("Please sign in to continue to the requested page.");
        return this.router.parseUrl('/');
      })
    );
  }
  
}
