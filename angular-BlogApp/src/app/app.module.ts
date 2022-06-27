import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ReactiveFormsModule } from '@angular/forms';
import { TabsModule } from 'ngx-bootstrap/tabs';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { PostsComponent } from './posts/posts.component';
import { PostComponent } from './posts/post/post.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { LoginRegisterComponent } from './home/login-register/login-register.component';
import { LoginFormComponent } from './home/login-register/login-form/login-form.component';
import { RegisterFormComponent } from './home/login-register/register-form/register-form.component';
import { UserDetailsComponent } from './user-details/user-details.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    PostsComponent,
    PostComponent,
    NotFoundComponent,
    HeaderComponent,
    FooterComponent,
    LoginRegisterComponent,
    LoginFormComponent,
    RegisterFormComponent,
    UserDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ModalModule.forRoot(),
    TabsModule.forRoot(),
    ReactiveFormsModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
