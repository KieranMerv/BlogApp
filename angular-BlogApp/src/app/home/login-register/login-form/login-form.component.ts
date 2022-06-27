import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
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

  userLoginVM: UserLoginVM = {
    email: '',
    password: ''
  };

  constructor(private usersApiCallsService: UsersApiCallsService, private router: Router) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.userLoginVM.email = this.loginForm.get('loginEmail')?.value;
    this.userLoginVM.password = this.loginForm.get('loginPassword')?.value;

    this.usersApiCallsService.login(this.userLoginVM).subscribe(() => {
      this.router.navigate(['/posts']);
    });
  }
}