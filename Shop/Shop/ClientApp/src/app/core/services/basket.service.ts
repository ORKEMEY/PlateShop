import { Injectable } from '@angular/core';
import Good from '../models/good.model';

@Injectable({ providedIn: 'root' })
export default class BasketService {
  public goods: Good[] = [];

  private readonly goodsKey: string = 'basket';

  constructor() {
    const res = localStorage.getItem(this.goodsKey);
    if (res != null) {
      this.goods = JSON.parse(res);
    }
  }

  private save(): void {
    localStorage.setItem(this.goodsKey, JSON.stringify(this.goods));
  }

  public addGood(good: Good) {
    this.goods.push(good);
    this.save();
  }

  public deleteGood(good: Good) {
    const id = this.goods.indexOf(good);
    if (id === -1) return;
    this.goods.splice(id, 1);
    this.save();
  }

  public deleteGoodById(id: Number) {
    const good = this.goods.find((g) => g.id === id);
    this.deleteGood(good);
  }

  public clearBasket() {
    this.goods.length = 0;
    this.save();
  }
}
