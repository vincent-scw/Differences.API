import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AccountRoutingModule } from './account-routing.module';
import { SharedModule } from '../shared/shared.module';

import { AccountComponent } from './account.component';
import { SigninComponent } from './signin/signin.component';
import { SignupComponent } from './signup/signup.component';

@NgModule({
  imports: [
    FormsModule,
    ReactiveFormsModule,
    AccountRoutingModule,
    SharedModule
  ],
  declarations: [
    AccountComponent,
    SigninComponent,
    SignupComponent
  ],
  exports: [
    AccountComponent
  ]
})
export class AccountModule { }
