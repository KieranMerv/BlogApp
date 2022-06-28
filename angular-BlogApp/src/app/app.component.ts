import { Component, OnInit } from '@angular/core';
import { User } from './_models/user.model';
import { UsersApiCallsService } from './_services/users-api-calls.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Primary Blog: App';

  constructor(private usersApiCallsService: UsersApiCallsService) { }

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');

    if (userString !== null) {
      const userParsed: User = JSON.parse(userString);
      this.usersApiCallsService.setCurrentUser(userParsed);
    }
  }
}
