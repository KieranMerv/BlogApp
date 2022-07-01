import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs';
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
    return this.http.get<PostVM[]>(this.baseApiUrl + "/posts/public").pipe(
      // Account for Timezone Offset. Note: Returns response returns date as a string!
      map(response => {
        return response.map(el => {
          el.created = new Date(el.created + 'Z');
          el.updated = new Date(el.updated + 'Z');
          return el;
        });
      })
    );
  }

  getUserPosts(): Observable<PostVM[]> {
    return this.http.get<PostVM[]>(this.baseApiUrl + "/posts").pipe(
      // Account for Timezone Offset. Note: Returns response returns date as a string!
      map(response => {
        return response.map(el => {
          el.created = new Date(el.created + 'Z');
          el.updated = new Date(el.updated + 'Z');
          return el;
        });
      })
    );;
  }

  getPostById(id: string): Observable<PostVM> {
    return this.http.get<PostVM>(this.baseApiUrl + "/posts/" + id).pipe(
      // Account for Timezone Offset. Note: Returns response returns date as a string!
      map(response => {
        response.created = new Date(response.created + 'Z');
        response.updated = new Date(response.updated + 'Z');
        return response;
      })
    );
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
