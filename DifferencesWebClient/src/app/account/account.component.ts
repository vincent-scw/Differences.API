import { Component, OnInit } from '@angular/core';
import { MdDialog, MdDialogRef, MD_DIALOG_DATA } from '@angular/material';
import { Observable } from 'rxjs/Observable';

import { AuthenticationService } from '../services/authentication.service';
import { SigninComponent } from './signin/signin.component';
import { User } from '../models/user';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html'
})

export class AccountComponent implements OnInit {
  isSignedIn: Observable<boolean>;
  currentUser: User;
  isAdmin: boolean;

  ngOnInit() {
    this.isSignedIn = this.authenticationService.isSignedIn();

    this.authenticationService.userChanged().subscribe(
        (user: User) => {
            this.currentUser = user;
            this.isAdmin = this.authenticationService.isInRole('administrator');
        });

    // Optional strategy for refresh token through a scheduler.
    this.authenticationService.startupTokenRefresh();
}

  constructor(public dialog: MdDialog,
    protected authenticationService: AuthenticationService) {}

  onSignInClicked(): void {
    const dialogRef = this.dialog.open(SigninComponent, {
      width: '400px'
    });
    dialogRef.afterClosed().subscribe(result => {
    });
  }

  onSignUpClicked() {

  }

  onSignoutCliecked() {
    this.authenticationService.signout();
  }
}
