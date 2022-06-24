import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { PostAddEditVM } from 'src/app/_models/post-addeditVM.model';
import { PostVM } from 'src/app/_models/post-VM.model';
import { ApiCallsService } from 'src/app/_services/api-calls.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {
  postForm = new FormGroup({
    postTitle: new FormControl(''),
    postBody: new FormControl(''),
    postIsPrivate: new FormControl(true)
  });
  postAddEditVM: PostAddEditVM = {
    title: '',
    body: '',
    created: new Date(Date.now()),
    updated: new Date(Date.now()),
    isPrivate: true
  };
  postVM: PostVM = {
    id: '',
    title: '',
    body: '',
    created: new Date(Date.now()),
    updated: new Date(Date.now()),
    isPrivate: true
  };
  modalRef?: BsModalRef;
  postId: string | null = null;
  editMode: boolean = false;
  
  constructor(private apiCallsService: ApiCallsService, 
    private bsModalService: BsModalService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.postId = this.route.snapshot.paramMap.get('id');

    if (this.postId !== null) {
        this.apiCallsService.getPostById(this.postId).subscribe(response => {
          this.postVM = response;

          this.postForm.setValue({
            postTitle: this.postVM.title,
            postBody: this.postVM.body,
            postIsPrivate: this.postVM.isPrivate
          });
        });
    } else {
      this.editMode = true;
    }
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
    this.postAddEditVM.isPrivate = this.postForm.get('postIsPrivate')?.value;
    this.postAddEditVM.updated = new Date(Date.now());
    if (this.postId == null) {
      this.postAddEditVM.created = new Date(Date.now());
      this.apiCallsService.createPost(this.postAddEditVM).subscribe(response => {
        this.router.navigate(['/posts', response]);
      });
    } else {
      this.apiCallsService.updatePost(this.postId, this.postAddEditVM).subscribe();
    }
  }

  deletePost() {
    if (this.postId != null) {
      this.apiCallsService.deletePost(this.postId).subscribe(() => {
        this.modalRef?.hide();
        this.router.navigate(['/posts']);
      });
    }
  }
}