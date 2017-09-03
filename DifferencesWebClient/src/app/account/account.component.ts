import { Component, OnInit } from '@angular/core';
import {MdDialog, MdDialogRef, MD_DIALOG_DATA} from '@angular/material';

import { SigninComponent } from './signin/signin.component';
import { User } from '../models/user';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html'
})

export class AccountComponent implements OnInit {
  isSignedIn: boolean;
  currentUser: User;

  ngOnInit(): void {
    this.isSignedIn = false;
    this.currentUser = {
      id: '123',
      userName: 'edentidus',
      nickName: 'vincent'
    }
  }

  constructor(public dialog: MdDialog) {}

  onSignIn(): void {
    const dialogRef = this.dialog.open(SigninComponent, {
      width: '400px'
    });
    dialogRef.afterClosed();
  }

  onSignUp() {

  }
}
