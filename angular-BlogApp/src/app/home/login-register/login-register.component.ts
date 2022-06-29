import { Component, OnInit, ViewChild } from '@angular/core';
import { TabsetComponent } from 'ngx-bootstrap/tabs';

@Component({
  selector: 'app-login-register',
  templateUrl: './login-register.component.html',
  styleUrls: ['./login-register.component.css']
})
export class LoginRegisterComponent implements OnInit {
  @ViewChild('loginRegisterTabs', {static: true}) loginRegisterTabs?: TabsetComponent;
  
  constructor() { }

  ngOnInit(): void {
  }

  selectTab(tabId: number) {
    if (this.loginRegisterTabs?.tabs[tabId]) {
      this.loginRegisterTabs.tabs[tabId].active = true;
    }
  }
}
