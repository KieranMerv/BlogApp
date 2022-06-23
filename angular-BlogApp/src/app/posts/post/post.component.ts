import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { PostAddEditVM } from 'src/app/_models/post-addeditVM.model';
import { ApiCallsService } from 'src/app/_services/api-calls.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {
  postForm = new FormGroup({
    postTitle: new FormControl(''),
    postBody: new FormControl('')
  });
  postAddEditVM: PostAddEditVM = {
    title: '',
    body: '',
    created: new Date(Date.now()),
    updated: new Date(Date.now())
  };
  modalRef?: BsModalRef;
  postId: string | null = null;
  editMode: boolean = false;
  
  constructor(private apiCallsService: ApiCallsService, 
    private bsModalService: BsModalService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params: ParamMap) => {
      this.postId = params.get('id');

      if (this.postId !== null) {
        this.apiCallsService.getPostById(this.postId).subscribe(response => {
          this.postAddEditVM = response;
        });

        this.postForm.setValue({
          postTitle: this.postAddEditVM.title,
          postBody: this.postAddEditVM.body,
        });
      } else {
        this.editMode = true;
      }
    });
  }

  toggleEditMode() {
    this.editMode = !this.editMode;
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.bsModalService.show(template);
  }

  onSubmit() {
    this.postAddEditVM.title = this.postForm.get('postTitle')?.value;
    this.postAddEditVM.body = this.postForm.get('postBody')?.value;
    this.postAddEditVM.updated = new Date(Date.now());
    if (this.postId == null) {
      this.postAddEditVM.created = new Date(Date.now());
      this.apiCallsService.createPost(this.postAddEditVM).subscribe(response => {
        console.log(response);
      });
    } else {
      this.apiCallsService.updatePost(this.postId, this.postAddEditVM).subscribe(response => {
        console.log(response);
      });
    }
  }

  deletePost() {
    if (this.postId != null) {
      this.apiCallsService.deletePost(this.postId).subscribe(response => {
        console.log(response);
      });
    }
  }
}