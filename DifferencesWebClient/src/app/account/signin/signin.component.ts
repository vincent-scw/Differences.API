import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AuthenticationService } from '../../services/authentication.service';
import { Signin } from '../signin';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html'
})
export class SigninComponent extends Signin {

    constructor(
        protected router: Router,
        protected authenticationService: AuthenticationService) {
        super(router, authenticationService);

        // Preloads data for live example.
        this.model.username = 'vincent';
        this.model.password = '12345';
    }

}
