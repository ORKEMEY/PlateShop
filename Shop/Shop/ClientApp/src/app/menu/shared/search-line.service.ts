import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';

@Injectable()
export default class SearchLineService {
  private searchLine: Subject<string>;

  private searchLineIsEmpty: Subject<void>;

  public searchLine$: Observable<string>;

  public searchLineIsEmpty$: Observable<void>;

  constructor() {
    this.searchLine = new Subject<string>();
    this.searchLineIsEmpty = new Subject();
    this.searchLine$ = this.searchLine.asObservable();
    this.searchLineIsEmpty$ = this.searchLineIsEmpty.asObservable();
  }

  public emitSearchLineValue(str: string) {
    this.searchLine.next(str);
  }

  public emitSearchLineIsEmpty() {
    this.searchLineIsEmpty.next();
  }
}
