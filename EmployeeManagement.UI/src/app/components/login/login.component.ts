import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  form: FormGroup;
  submitted = false;

  constructor(
    private auth: AuthService,
    private fb: FormBuilder,
    private router: Router
  ) {
    this.form = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(5)]]
    });
  }

  login() {
    debugger;
    this.submitted = true;

    if (!this.form.invalid) {
      return;
    }

    this.auth.login(this.form.value).subscribe({
      next: (res) => {
        this.auth.setToken(res.token);
        this.router.navigate(['/employees']);
      },
      error: () => alert('Login failed')
    });
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
  get f() {
    return this.form.controls;
  }
}
