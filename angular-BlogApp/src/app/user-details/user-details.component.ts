import { Component, OnInit, TemplateRef } from '@angular/core';
import { UserUpdateVM } from '../_models/user-updateVM.model';
import { User } from '../_models/user.model';
import { UsersApiCallsService } from '../_services/users-api-calls.service';
import { take } from 'rxjs';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { UserDeleteVM } from '../_models/user-deleteVM.model';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CustomValidators } from '../_helper/custom-validators.helper';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {
  userUpdateForm = new FormGroup({
    userUserName: new FormControl(''),
    userAlias: new FormControl(''),
    userEmail: new FormControl(''),
    userNewEmail: new FormControl('', Validators.email),
    userPassword: new FormControl('', Validators.required),
    userNewPassword: new FormControl(''),
    userNewConfirmPassword: new FormControl('')
  },
  CustomValidators.passwordMatchConfirm('userNewPassword', 'userNewConfirmPassword')
  );

  confirmDeleteInput = new FormControl('');
  userUpdateVM: UserUpdateVM = {
    userName: '',
    alias: '',
    email: '',
    newEmail: null,
    password: '',
    newPassword: null,
    newConfirmPassword: null
  };

  userDeleteVM: UserDeleteVM = {
    email: '',
    password: ''
  };

  currentUser: User | null = null;
  modalRef?: BsModalRef;
  editMode: boolean = false;
  validationErrors: string[] = [];
  busyStatusUpdate = false;
  busyStatusDelete = false;

  constructor(
    private usersApiCallsService: UsersApiCallsService, 
    private bsModalService: BsModalService,
    private router: Router,
    private toastr: ToastrService) {
      this.usersApiCallsService.currentUser$.pipe(take(1)).subscribe(response => {
        this.currentUser = response;
      });
  }

  ngOnInit(): void {
    this.userUpdateForm.controls['userNewPassword'].valueChanges.subscribe(() => {
      this.userUpdateForm.controls['userNewConfirmPassword'].updateValueAndValidity();
    });

    this.userUpdateForm.get('userUserName')?.setValue(this.currentUser?.userName);
    this.userUpdateForm.get('userAlias')?.setValue(this.currentUser?.alias);
    if (typeof(this.currentUser?.token) === 'string')
      this.userUpdateForm.get('userEmail')?.setValue(this.usersApiCallsService.getDecodedToken(this.currentUser?.token)['email']);
  }

  toggleEditMode() {
    this.editMode = !this.editMode;
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.bsModalService.show(template);
  }

  onSubmit() {
    this.userUpdateVM.userName = this.userUpdateForm.get('userUserName')?.value;
    this.userUpdateVM.alias = this.userUpdateForm.get('userAlias')?.value;
    this.userUpdateVM.email = this.userUpdateForm.get('userEmail')?.value;

    const userNewEmail = this.userUpdateForm.get('userNewEmail')?.value;
    if (userNewEmail === null || userNewEmail === '')
      this.userUpdateVM.newEmail = null;
    else
      this.userUpdateVM.newEmail = userNewEmail;

    this.userUpdateVM.password = this.userUpdateForm.get('userPassword')?.value;

    const userNewPassword = this.userUpdateForm.get('userNewPassword')?.value;

    const userNewConfirmPassword = this.userUpdateForm.get('userNewConfirmPassword')?.value;

    if (userNewPassword !== null && userNewPassword !== '') {
      if (userNewPassword === userNewConfirmPassword) {
        this.userUpdateVM.newPassword = userNewPassword;
        this.userUpdateVM.newConfirmPassword = userNewConfirmPassword;
      } else {
        this.validationErrors.push('New password must match confirmation new password. Update aborted.');
        return;
      }
    }
    else {
      this.userUpdateVM.newPassword = null;
      this.userUpdateVM.newConfirmPassword = null;
    }
      

    this.busyStatusUpdate = true;
    this.usersApiCallsService.updateUser(this.userUpdateVM).subscribe({
      next: () => {
        this.busyStatusUpdate = false;
        this.toastr.success('User updated!');
        this.router.navigate(['/user/details']);
      },
      error: error => {
        this.busyStatusUpdate = false;
        this.validationErrors = error;
      }
    });
  }

  deleteUser() {
    if (this.confirmDeleteInput.value === 'dEleTe m*E') {
      if (typeof (this.currentUser?.token) === 'string')
        this.userDeleteVM.email = this.usersApiCallsService.getDecodedToken(this.currentUser?.token)['email'];

      this.userDeleteVM.password = this.userUpdateForm.get('userPassword')?.value;

      this.busyStatusDelete = true;
      this.usersApiCallsService.deleteUser(this.userDeleteVM).subscribe({
        next: () => {
          this.modalRef?.hide();
          this.busyStatusDelete = false;
          this.toastr.success('User deleted!');
          this.router.navigate(['/']);
        },
        error: () => {
          this.busyStatusDelete = false;
        }
      });
    } else {
      this.toastr.error('User deletion failed. Delete confirmation input incorrect.');
    }
  }
}
