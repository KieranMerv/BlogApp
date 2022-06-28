import { Component, OnInit } from '@angular/core';
import { PostVM } from '../_models/post-VM.model';
import { ApiCallsService } from '../_services/api-calls.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  postVMPublicCollection: PostVM[] = [];

  constructor(private apiCallsService: ApiCallsService) { }

  ngOnInit(): void {
    this.apiCallsService.getPublicPosts().subscribe(response => {
      this.postVMPublicCollection = response;
    });
  }

}
