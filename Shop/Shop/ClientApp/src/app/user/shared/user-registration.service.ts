import { Injectable } from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators,
  ValidationErrors,
  AbstractControl,
} from '@angular/forms';
import { Router } from '@angular/router';
import { Observer } from 'rxjs';
import UserService from '../../core/services/user.service';
import User from '../../core/models/user.model';

@Injectable()
export default class UserRegistrationService {
  public readonly form: FormGroup;

  constructor(private userService: UserService, private router: Router) {
    this.form = new FormGroup({
      Login: new FormControl('', Validators.required),
      Passwords: new FormGroup(
        {
          Password: new FormControl('', [
            Validators.required,
            Validators.minLength(5),
            this.SpecialCharactersPasswordValidator,
          ]),

          ConfirmPassword: new FormControl('', [Validators.required]),
        },
        this.PasswordsMatchValidator,
      ),
    });
  }

  public SpecialCharactersPasswordValidator(control: FormControl): ValidationErrors | null {
    let regexp = new RegExp('[0-9]+');
    if (!regexp.test(control.value)) {
      return { specialcharacters: { cotainsSpecialCharacters: false } };
    }

    regexp = new RegExp('[A-Za-z]+');
    if (!regexp.test(control.value)) {
      return { specialcharacters: { cotainsSpecialCharacters: false } };
    }

    return null;
  }

  public PasswordsMatchValidator(control: AbstractControl): ValidationErrors | null {
    if (control.get('Password').value !== control.get('ConfirmPassword').value) {
      return { passwordsmatch: { passwordsdontmatch: true } };
    }
    return null;
  }

  public ValidateLogin(): string | null {
    if (this.form.controls.Login.valid || this.form.controls.Login.pristine) {
      return null;
    }
    let res: string | null = null;
    if (this.form.controls.Login.errors?.required) {
      res = "Login cann't be empty!";
    }
    return res;
  }

  public ValidatePassword(): string | null {
    if (
      this.form.controls.Passwords.get('Password').valid ||
      this.form.controls.Passwords.get('Password').pristine
    ) {
      return null;
    }

    let res: string | null = null;
    if (this.form.controls.Passwords.get('Password').errors?.required) {
      res = "Password cann't be empty!";
    } else if (this.form.controls.Passwords.get('Password').errors?.minlength) {
      res = "Password's minimum number of characters is 5!";
    } else if (this.form.controls.Passwords.get('Password').errors?.specialcharacters) {
      res = 'Password has to contain both letters and digits!';
    }
    return res;
  }

  public ValidatePasswordConfirmation(): string | null {
    if (this.form.controls.Passwords.get('ConfirmPassword').pristine) {
      return null;
    }
    let res: string | null = null;
    if (this.form.controls.Passwords.get('ConfirmPassword').errors?.required) {
      res = "Password confirmation cann't be empty!";
    } else if (this.form.controls.Passwords.errors?.passwordsmatch) {
      res = 'Password confirmation must match password!';
    }

    if (this.form.controls.Passwords.get('ConfirmPassword').valid) {
      return res;
    }
    return res;
  }

  submit(observer: Observer<void>) {
    if (!this.form.valid) {
      throw new Error('submit on invalid form');
    } else {
      this.userService.post(
        new User(
          this.form.controls.Login.value,
          this.form.controls.Passwords.get('Password').value,
        ),
        {
          next: () => {
            observer?.next?.();
            this.router.navigate(['/menus']);
          },
          error: (msg) => observer?.error?.(msg),
          complete: () => observer?.complete?.(),
        } as Observer<void>,
      );
    }
  }
}
