import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, Observer } from 'rxjs';
import { map } from 'rxjs/operators';
import CredentialsService from './credentials.service';

import Order from '../models/order.model';

@Injectable({ providedIn: 'root' })
export default class OrderService {
  private dataOrders: BehaviorSubject<Order[] | null>;

  public dataOrders$: Observable<Order[] | null>;

  private get userId(): Number {
    return this.credentialsService.getUserId();
  }

  constructor(private http: HttpClient, private credentialsService: CredentialsService) {
    this.dataOrders = new BehaviorSubject<Order[] | null>(null);
    this.dataOrders$ = this.dataOrders.asObservable();
  }

  public getById(id: number, observer?: Observer<Order>) {
    /* this.refreshOrders();
    this.dataOrders$.subscribe({
      next: (data: Order[]) => {
        observer?.next?.(data[0]);
      },
    }); */

    this.http
      .get(`api/Orders/${id}`)
      .pipe(map((data) => data as Order))
      .subscribe({
        next: (data) => observer?.next?.(data),
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

  public getByUserId() {
    /* this.refreshOrders();
    this.dataOrders$.subscribe({
      next: (data: Order[]) => {
        observer?.next?.(data);
      },
    }); */

    this.http
      .get(`api/Orders/ByUserId/${this.userId}`)
      .pipe(map((data) => data as Order[]))
      .subscribe({
        next: (data: Order[]) => this.dataOrders.next(data),
        error: (err) => {
          console.error(err);
          this.dataOrders.next(null);
        },
      });
  }

  public searchOrdersByAddress(address: string) {
    /*
    this.http.get('../../assets/orders.json').subscribe({
      next: (data: Order[]) =>
        this.dataOrders.next(data.filter((el) => el.address.includes(address))),
      error: (err) => {
        console.error(err);
        this.dataOrders.next(null);
      },
    }); */

    this.http
      .get(`api/Orders/ByUserId/${this.userId}`)
      .pipe(map((data) => data as Order[]))
      .subscribe({
        next: (data: Order[]) =>
          this.dataOrders.next(data.filter((el) => el.address.includes(address))),
        error: (err) => {
          console.error(err);
          this.dataOrders.next(null);
        },
      });
  }

  public post(order: Order, observer?: Observer<void>) {
    this.http.post(`api/Orders`, order).subscribe({
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

  public ClearData() {
    this.dataOrders.next(null);
  }
}
