/* eslint-disable no-bitwise */
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observer } from 'rxjs';
import { tap } from 'rxjs/operators';
import { MD5, SHA256 } from 'crypto-js';
import User from '../models/user.model';
import CredentialsService, { Credentials } from './credentials.service';
import LogOutService from './log-out.service';

@Injectable({ providedIn: 'root' })
export default class UserService {
  constructor(
    private http: HttpClient,
    private credentialsService: CredentialsService,
    private logOutService: LogOutService,
  ) {}

  public getToken(user: User, observer: Observer<void>) {
    this.HashPassword(user);
    return this.http
      .post('api/Users/token', user)
      .pipe(
        tap({
          next: (res: any) => {
            this.credentialsService.saveCredentials({
              accessToken: res.accessToken,
              userId: res.userId,
              login: res.login,
              role: res.role,
              refreshToken: res.refreshToken,
            } as Credentials);
          },
        }),
      )
      .subscribe({
        next: () => observer?.next?.(),
        error: (err) => {
          if (err.status === 400) {
            observer?.error?.(err.error.errorText);
          } else {
            console.error(err);
          }
        },
        complete: () => observer?.complete?.(),
      });
  }

  public refreshToken(observer?: Observer<void>) {
    const credentials = this.credentialsService.getCredentials();
    return this.http
      .post('api/Users/refresh', credentials)
      .pipe(
        tap({
          next: (res: any) => {
            this.credentialsService.saveCredentials({
              accessToken: res.accessToken,
              userId: res.userId,
              login: res.login,
              role: res.role,
              refreshToken: res.refreshToken,
            } as Credentials);
          },
        }),
      )
      .subscribe({
        next: () => observer?.next?.(),
        error: (err) => observer?.error?.(err),
        complete: () => observer?.complete?.(),
      });
  }

  public post(user: User, observer: Observer<void>) {
    this.HashPassword(user);
    return this.http
      .post('api/Users', user)
      .pipe(
        tap({
          next: (res: any) => {
            this.credentialsService.saveCredentials({
              accessToken: res.accessToken,
              userId: res.userId,
              login: res.login,
              role: res.role,
              refreshToken: res.refreshToken,
            } as Credentials);
          },
        }),
      )
      .subscribe({
        next: () => observer?.next?.(),
        error: (err) => {
          if (err.status === 400) {
            observer?.error?.(err.error.errorText);
          } else {
            console.error(err);
          }
        },
        complete: () => observer?.complete?.(),
      });
  }

  public logOut() {
    this.logOutService.LogOut();
  }

  private async HashPassword(user: User) {
    const sol = MD5(user.Login).toString();
    const pas = MD5(user.Password).toString();

    const concat = [...sol].map((el, ind): number => el.charCodeAt(0) ^ pas.charCodeAt(ind));

    const res = String.fromCharCode(...concat);

    user.Password = SHA256(res).toString();
    return user;
  }
}
