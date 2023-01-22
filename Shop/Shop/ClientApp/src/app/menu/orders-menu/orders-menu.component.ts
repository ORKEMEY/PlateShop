import { Component, OnInit, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { Subscription } from 'rxjs';
import OrderService from '../../core/services/order.service';
import Order from '../../core/models/order.model';
import Paginator from '../../shared/paginator';
import Alert from '../../core/alert';

@Component({
  selector: 'orders-menu-component',
  templateUrl: './orders-menu.component.html',
  styleUrls: ['./orders-menu.component.css'],
})
export default class OrdersMenuComponent extends Paginator<Order> implements OnInit, OnDestroy {
  private ordersSub: Subscription;

  public orders: Order[] | null;

  protected get items(): Order[] | null {
    return this.orders;
  }

  @ViewChild('alertDiv', { static: false })
  alertDiv: ElementRef | undefined;

  constructor(private orderService: OrderService) {
    super();
  }

  ngOnInit(): void {
    this.ordersSub = this.orderService.dataOrders$.subscribe((data: Order[] | null) => {
      this.orders = data;
      this.toFirstPage();
      this.checkCollection();
    });
    this.orderService.getByUserId();
  }

  ngOnDestroy(): void {
    this.ordersSub.unsubscribe();
  }

  log(order: Order) {
    console.log(order);
  }

  private checkCollection() {
    if (this.orders === null || this.orders.length === 0) {
      Alert.alertMessage(this.alertDiv, 'No orders found');
    } else {
      Alert.hideAlertMessage(this.alertDiv);
    }
  }
}
