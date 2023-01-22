import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, Observer } from 'rxjs';
import { map } from 'rxjs/operators';

import Good from '../models/good.model';

@Injectable({ providedIn: 'root' })
export default class GoodService {
  private dataGoods: BehaviorSubject<Good[] | null>;

  public dataGoods$: Observable<Good[] | null>;

  constructor(private http: HttpClient) {
    this.dataGoods = new BehaviorSubject<Good[] | null>(null);
    this.dataGoods$ = this.dataGoods.asObservable();
  }

  public getById(id: number, observer?: Observer<Good>) {
    /* this.refreshGoods();
    this.dataGoods$.subscribe({
      next: (data: Good[]) => {
        observer?.next?.(data[0]);
      },
    }); */

    this.http
      .get(`api/Goods/${id}`)
      .pipe(map((data) => data as Good))
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

  public refreshGoods() {
    /* this.http.get('../../assets/goods.json').subscribe({
      next: (data: Good[]) => this.dataGoods.next(data),
      error: (err) => {
        console.error(err);
        this.dataGoods.next(null);
      },
    }); */

    this.http
      .get('api/Goods')
      .pipe(map((data) => data as Good[]))
      .subscribe({
        next: (data: Good[]) => this.dataGoods.next(data),
        error: (err) => {
          console.error(err);
          this.dataGoods.next(null);
        },
      });
  }

  public searchGoodsByName(name: string) {
    /* this.http.get('../../assets/goods.json').subscribe({
      next: (data: Good[]) => this.dataGoods.next(data.filter((el) => el.name.includes(name))),
      error: (err) => {
        console.error(err);
        this.dataGoods.next(null);
      },
    }); */

    this.http
      .get('api/Goods')
      .pipe(map((data) => data as Good[]))
      .subscribe({
        next: (data: Good[]) => this.dataGoods.next(data.filter((el) => el.name.includes(name))),
        error: (err) => {
          console.error(err);
          this.dataGoods.next(null);
        },
      });
  }

  public post(good: Good, observer?: Observer<void>) {
    return this.http.post('api/Goods', good).subscribe({
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
    this.dataGoods.next(null);
  }
}
