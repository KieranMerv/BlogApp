import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { PostAddEditVM } from '../_models/post-addeditVM.model';
import { PostVM } from '../_models/post-VM.model';

@Injectable({
  providedIn: 'root'
})
export class ApiCallsService {
  baseApiUrl = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  getPublicPosts(): Observable<PostVM[]> {
    return this.http.get<PostVM[]>(this.baseApiUrl + "/posts/public");
  }

  getUserPosts(): Observable<PostVM[]> {
    return this.http.get<PostVM[]>(this.baseApiUrl + "/posts");
  }

  getPostById(id: string): Observable<PostVM> {
    return this.http.get<PostVM>(this.baseApiUrl + "/posts/" + id);
  }

  createPost(postAddEditVM: PostAddEditVM): Observable<string> {
    return this.http.post<string>(this.baseApiUrl + "/posts", postAddEditVM);
  }

  updatePost(id: string, postAddEditVM: PostAddEditVM): Observable<void> {
    return this.http.put<void>(this.baseApiUrl + "/posts/" + id, postAddEditVM);
  }

  deletePost(id: string): Observable<void> {
    return this.http.delete<void>(this.baseApiUrl + "/posts/" + id);
  }
}
