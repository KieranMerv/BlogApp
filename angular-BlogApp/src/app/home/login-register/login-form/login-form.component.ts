import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserLoginVM } from 'src/app/_models/user-loginVM.model';
import { User } from 'src/app/_models/user.model';
import { UsersApiCallsService } from 'src/app/_services/users-api-calls.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {
  currentUser: User | null = null;
  loginForm = new FormGroup({
    loginEmail: new FormControl('', [Validators.required, Validators.email]),
    loginPassword: new FormControl('', Validators.required)
  });

  validationErrors: string[] = [];
  busyStatusLogin = false;

  userLoginVM: UserLoginVM = {
    email: '',
    password: ''
  };

  constructor(
    public usersApiCallsService: UsersApiCallsService, 
    private router: Router) {
      this.usersApiCallsService.currentUser$.pipe(take(1)).subscribe(response => {
        this.currentUser = response;
      });
  }

  ngOnInit(): void {
  }

  onSubmit() {
    this.userLoginVM.email = this.loginForm.get('loginEmail')?.value;
    this.userLoginVM.password = this.loginForm.get('loginPassword')?.value;

    this.busyStatusLogin = true;
    this.usersApiCallsService.loginUser(this.userLoginVM).subscribe({
      next: () => {
        this.busyStatusLogin = false;
        this.router.navigate(['/posts']);
      },
      error: error => {
        this.busyStatusLogin = false;
        this.validationErrors = error;
      }
    });
  }

  logout() {
    this.usersApiCallsService.logoutUser();
  }
}