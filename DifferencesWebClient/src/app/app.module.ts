import { BrowserModule, Title } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule, Http } from '@angular/http';
import { QuillModule } from 'ngx-quill';

import { AppRoutingModule } from './app-routing.module';
import { LayoutModule } from './layout/layout.module';
import { SharedModule } from './shared/shared.module';
import { QuestionsModule } from './questions/questions.module';
import { ArticlesModule } from './articles/articles.module';
import { UsersModule } from './users/users.module';

import { AuthGuard } from './services/auth.guard';
import { AuthenticationService } from './services/authentication.service';
import { IdentityService } from './services/identity.service';
import { BrowserStorage } from './services/browser-storage.service';
import { ApiClientService } from './services/api-client.service';

import { AppComponent } from './app.component';

// angular2-jwt config for JiT and AoT compilation.
import { AuthHttp, AuthConfig } from 'angular2-jwt';

// Set tokenGetter to use the same storage in AuthenticationService.Helpers.
export function getAuthHttp(http: Http) {
  return new AuthHttp(new AuthConfig({
      noJwtError: true,
      tokenGetter: (() => localStorage.getItem('access_token'))
  }), http);
}

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    BrowserModule,
    BrowserAnimationsModule,
    QuillModule,
    SharedModule,
    LayoutModule,
    QuestionsModule,
    ArticlesModule,
    UsersModule
  ],
  providers: [
    Title,
    AuthGuard,
    AuthenticationService,
    IdentityService,
    ApiClientService,
    BrowserStorage,
    {
      provide: AuthHttp,
      useFactory: getAuthHttp,
      deps: [Http]
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
