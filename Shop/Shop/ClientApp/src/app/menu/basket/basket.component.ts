import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import BasketService from '../../core/services/basket.service';
import Good from '../../core/models/good.model';
import Paginator from '../../shared/paginator';
import Alert from '../../core/alert';

@Component({
  selector: 'basket-component',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.css'],
})
export default class BasketComponent extends Paginator<Good> implements OnInit {
  private goodsarr: Good[] | null;

  public get goods(): Good[] | null {
    this.checkCollection();
    return this.goodsarr;
  }

  public set goods(val: Good[] | null) {
    this.goodsarr = val;
  }

  public orderFormVisibility: boolean = false;

  public get basketCost(): Number {
    let sum: number = 0;
    this.goods.forEach((element) => {
      sum += element.cost as number;
    });
    return sum;
  }

  protected get items(): Good[] | null {
    return this.goods;
  }

  @ViewChild('alertDiv', { static: false })
  alertDiv: ElementRef | undefined;

  constructor(private basketService: BasketService) {
    super();
  }

  removeGood(id: number) {
    this.basketService.deleteGoodById(id);
  }

  ngOnInit(): void {
    this.goods = this.basketService.goods;
    this.toFirstPage();
  }

  log(good: Good) {
    console.log(good);
  }

  buyBtnClick() {
    this.checkCollection();
    if (this.goods === null || this.goods.length === 0) {
      this.orderFormVisibility = false;
    } else {
      this.orderFormVisibility = true;
    }
  }

  private checkCollection() {
    if (this.goodsarr === null || this.goodsarr.length === 0) {
      Alert.alertMessage(this.alertDiv, 'Basket is empty!');
    } else {
      Alert.hideAlertMessage(this.alertDiv);
    }
  }
}
