import { Component, OnInit, TemplateRef } from '@angular/core';
import { UserUpdateVM } from '../_models/user-updateVM.model';
import { User } from '../_models/user.model';
import { UsersApiCallsService } from '../_services/users-api-calls.service';
import { take } from 'rxjs';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { UserDeleteVM } from '../_models/user-deleteVM.model';
import { Router } from '@angular/router';

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
    userNewEmail: new FormControl(''),
    userPassword: new FormControl('', Validators.required),
    userNewPassword: new FormControl('')
  });
  userUpdateVM: UserUpdateVM = {
    userName: '',
    alias: '',
    email: '',
    newEmail: null,
    password: '',
    newPassword: null
  };
  userDeleteVM: UserDeleteVM = {
    email: '',
    password: ''
  }
  currentUser: User | null = null;
  modalRef?: BsModalRef;
  editMode: boolean = false;
  confirmDeleteInput = new FormControl('');

  constructor(
    private usersApiCallsService: UsersApiCallsService, 
    private bsModalService: BsModalService,
    private router: Router) {
      this.usersApiCallsService.currentUser$.pipe(take(1)).subscribe(response => {
        this.currentUser = response;
      });
  }

  ngOnInit(): void {
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
    if (userNewPassword === null || userNewPassword === '')
      this.userUpdateVM.newPassword = null;
    else
      this.userUpdateVM.newPassword = userNewPassword;

    this.usersApiCallsService.updateUser(this.userUpdateVM).subscribe(() => {
      this.router.navigate(['/user/details']);
    });
  }

  deleteUser() {
    if (this.confirmDeleteInput.value === 'dEleTe m*E') {
      if (typeof (this.currentUser?.token) === 'string')
        this.userDeleteVM.email = this.usersApiCallsService.getDecodedToken(this.currentUser?.token)['email'];

      this.userDeleteVM.password = this.userUpdateForm.get('userPassword')?.value;
      this.usersApiCallsService.deleteUser(this.userDeleteVM).subscribe(() => {
        this.modalRef?.hide();
        this.router.navigate(['/']);
      });
    } else {
      console.log("Deletion of user failed. Delete confirmation input did not match requested value.");
    }
  }
}
