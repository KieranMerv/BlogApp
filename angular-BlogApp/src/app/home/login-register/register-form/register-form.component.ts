import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CustomValidators } from 'src/app/_helper/custom-validators.helper';
import { UserRegisterVM } from 'src/app/_models/user-registerVM.model';
import { UsersApiCallsService } from 'src/app/_services/users-api-calls.service';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.css']
})
export class RegisterFormComponent implements OnInit {
  registerForm = new FormGroup({
    registerUserName: new FormControl('', Validators.required),
    registerAlias: new FormControl(''),
    registerEmail: new FormControl('', [Validators.required, Validators.email]),
    registerPassword: new FormControl('', Validators.required),
    registerConfirmPassword: new FormControl('', Validators.required)
    },
    CustomValidators.passwordMatchConfirm('registerPassword', 'registerConfirmPassword')
  );

  userRegisterVM: UserRegisterVM = {
    userName: '',
    alias: null,
    email: '',
    password: '',
    confirmPassword: ''
  }

  validationErrors: string[] = [];
  busyStatusRegister = false;

  constructor(private usersApiCallsService: UsersApiCallsService, 
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.userRegisterVM.userName = this.registerForm.get('registerUserName')?.value;
    this.userRegisterVM.alias = this.registerForm.get('registerAlias')?.value;
    this.userRegisterVM.email = this.registerForm.get('registerEmail')?.value;
    this.userRegisterVM.password = this.registerForm.get('registerPassword')?.value;
    this.userRegisterVM.confirmPassword = this.registerForm.get('registerConfirmPassword')?.value;

    this.busyStatusRegister = true;
    this.usersApiCallsService.registerUser(this.userRegisterVM).subscribe({
      next: () => {
        this.busyStatusRegister = false;
        this.toastr.success('User registered!');
        this.router.navigate(['/posts']);
      },
      error: error => {
        this.busyStatusRegister = false;
        this.validationErrors = error;
      }
    });
  }
}
