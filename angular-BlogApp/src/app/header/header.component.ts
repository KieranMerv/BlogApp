import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user.model';
import { UsersApiCallsService } from '../_services/users-api-calls.service';
import { take } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  currentUser: User | null = null;

  constructor(
    private usersApiCallsService: UsersApiCallsService,
    private toastr: ToastrService) {
    this.usersApiCallsService.currentUser$.pipe(take(1)).subscribe(response => {
      this.currentUser = response;
    });
  }

  ngOnInit(): void {
  }

  logout() {
    this.usersApiCallsService.logoutUser().subscribe({
      next: response => {
        console.log(response);
        this.toastr.success('User logged out.');
      },
      error: error => {
        console.log(error);
        this.toastr.error('User logout failed.');
      }
    });
  }
}
