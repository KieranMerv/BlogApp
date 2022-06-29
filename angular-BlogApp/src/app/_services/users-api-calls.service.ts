import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UsersApiCallsService {
  baseUrl = environment.baseApiUrl;
  private currentUserSource = new ReplaySubject<User|null>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(
    private http: HttpClient,
    private router: Router,
    private toastr: ToastrService) { }

  loginUser(model: any) {
    return this.http.post<any>(this.baseUrl + "/users/login", model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  logoutUser() {
    // No need to make a call to server.
    localStorage.removeItem("user");
    this.currentUserSource.next(null);
    this.toastr.success('User logged out.');
    this.router.navigate(['/']);
  }

  registerUser(model: any) {
    return this.http.post<any>(this.baseUrl + "/users/register", model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  updateUser(model: any) {
    return this.http.put<any>(this.baseUrl + "/users/update", model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  deleteUser(model: any) {
    return this.http.delete<any>(this.baseUrl + "/users/delete", {body: model}).pipe(
      map(() => {
        localStorage.removeItem("user");
        this.currentUserSource.next(null);
      })
    );
  }

  setCurrentUser(user: User) {
    user.roles = [];
    const roles = this.getDecodedToken(user.token).role;
    Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);
    localStorage.setItem("user", JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  getDecodedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
  }
}