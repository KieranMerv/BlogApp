import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { PostComponent } from './posts/post/post.component';
import { PostsComponent } from './posts/posts.component';
import { UserDetailsComponent } from './user-details/user-details.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path:'', runGuardsAndResolvers: 'always', canActivate:[AuthGuard], children:[
    {path: 'posts', component: PostsComponent, pathMatch: 'full'},
    {path: 'posts/new', component: PostComponent, pathMatch: 'full'},
    {path: 'posts/:id', component: PostComponent, pathMatch: 'full'},
    {path: 'user/details', component: UserDetailsComponent, pathMatch: 'full'},
  ]},
  {path: '**', component: NotFoundComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
