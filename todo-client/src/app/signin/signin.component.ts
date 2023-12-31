import { Component } from '@angular/core';
import { LoginService } from '../../services/login.service';

@Component({
  selector: 'app-signin',
  standalone: true,
  imports: [],
  templateUrl: './signin.component.html',
  styleUrl: './signin.component.css',
  providers: [LoginService],
})
export class SigninComponent {
  constructor(private loginService: LoginService) {}

  login() {
    this.loginService.login();
  }
}
