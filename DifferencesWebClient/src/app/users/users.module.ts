import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { QuillModule } from 'ngx-quill';

import { UsersRoutingModule } from './users-routing.module';

import { UserListComponent } from './user-list.component';
import { UserDetailComponent } from './user-detail.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    QuillModule,
    UsersRoutingModule
  ],
  declarations: [
    UserListComponent,
    UserDetailComponent
  ],
  providers: []
})
export class UsersModule {}
