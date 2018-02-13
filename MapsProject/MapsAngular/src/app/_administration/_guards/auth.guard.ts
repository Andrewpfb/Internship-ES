import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { AuthenticationService } from '../_services/authentication.service';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(
    private router: Router,
    private authService: AuthenticationService
  ) { }

  canActivate() {
    if (this.authService.isAuthenticated()) {
      return true;
    } else {
      this.router.navigate(['auth']);
      return false;
    }
  }
}