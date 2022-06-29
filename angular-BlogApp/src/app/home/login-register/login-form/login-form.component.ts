import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserLoginVM } from 'src/app/_models/user-loginVM.model';
import { UsersApiCallsService } from 'src/app/_services/users-api-calls.service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {
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
    private usersApiCallsService: UsersApiCallsService, 
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.userLoginVM.email = this.loginForm.get('loginEmail')?.value;
    this.userLoginVM.password = this.loginForm.get('loginPassword')?.value;

    this.busyStatusLogin = true;
    this.usersApiCallsService.loginUser(this.userLoginVM).subscribe({
      next: response => {
        console.log(response);
        this.busyStatusLogin = false;
        this.router.navigate(['/posts']);
      },
      error: error => {
        this.busyStatusLogin = false;
        this.validationErrors = error;
      }
    });
  }
}