import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { PostVM } from '../_models/post-VM.model';
import { ApiCallsService } from '../_services/api-calls.service';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {
  postVMCollection: PostVM[] = [];
  busyStatus = false;

  currentDeleteId: string = '';

  constructor(private apiCallsService: ApiCallsService) { }

  ngOnInit(): void {
    this.busyStatus = true;
    this.apiCallsService.getUserPosts().subscribe({
      next: response => {
        this.busyStatus = false;
        this.postVMCollection = response;
      },
      error: () => {
        this.busyStatus = false;
      }
    });
  }
}
