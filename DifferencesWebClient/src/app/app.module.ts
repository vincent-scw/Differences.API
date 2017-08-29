import { BrowserModule, Title } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule, Http } from '@angular/http';

import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared/shared.module';

import { AuthGuard } from './services/auth.guard';
import { AuthenticationService } from './services/authentication.service';
import { IdentityService } from './services/identity.service';
import { BrowserStorage } from './services/browser-storage.service';

import { AppComponent } from './app.component';
import { SigninComponent } from './account/signin/signin.component';
import { SignupComponent } from './account/signup/signup.component';

// angular2-jwt config for JiT and AoT compilation.
import { AuthHttp, AuthConfig } from 'angular2-jwt';

// Set tokenGetter to use the same storage in AuthenticationService.Helpers.
export function getAuthHttp(http: Http) {
  return new AuthHttp(new AuthConfig({
      noJwtError: true,
      tokenGetter: (() => localStorage.getItem("id_token"))
  }), http);
}

@NgModule({
  declarations: [
    AppComponent,
    SigninComponent,
    SignupComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    FormsModule,
    HttpModule,
    BrowserModule,
    BrowserAnimationsModule,
    SharedModule
  ],
  providers: [
    Title,
    AuthGuard,
    AuthenticationService,
    IdentityService,
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
