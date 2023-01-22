import { Injectable } from '@angular/core';
import CredentialsService from './credentials.service';
import GoodService from './good.service';
import BasketService from './basket.service';
import OrderService from './order.service';

@Injectable({ providedIn: 'root' })
export default class LogOutService {
  constructor(
    private credentialsService: CredentialsService,
    private goodService: GoodService,
    private basketService: BasketService,
    private orderService: OrderService,
  ) {}

  public LogOut() {
    this.credentialsService.deleteCredentials();
    this.goodService.ClearData();
    this.orderService.ClearData();
    this.basketService.clearBasket();
  }
}
