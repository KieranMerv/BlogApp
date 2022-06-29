import { Component, OnInit, ViewChild } from '@angular/core';
import { TabsetComponent } from 'ngx-bootstrap/tabs';
import { User } from 'src/app/_models/user.model';
import { UsersApiCallsService } from 'src/app/_services/users-api-calls.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-login-register',
  templateUrl: './login-register.component.html',
  styleUrls: ['./login-register.component.css']
})
export class LoginRegisterComponent implements OnInit {
  @ViewChild('loginRegisterTabs', {static: true}) loginRegisterTabs?: TabsetComponent;
  currentUser: User | null = null;
  
  constructor(public usersApiCallsService: UsersApiCallsService) {
    this.usersApiCallsService.currentUser$.pipe(take(1)).subscribe(response => {
      this.currentUser = response;
    });
  }

  ngOnInit(): void {
  }

  selectTab(tabId: number) {
    if (this.loginRegisterTabs?.tabs[tabId]) {
      this.loginRegisterTabs.tabs[tabId].active = true;
    }
  }
}
