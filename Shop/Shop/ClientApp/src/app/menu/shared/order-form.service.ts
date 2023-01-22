import { Injectable } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Observer } from 'rxjs';
import OrderService from '../../core/services/order.service';
import CredentialsService from '../../core/services/credentials.service';
import BasketService from '../../core/services/basket.service';
import Order from '../../core/models/order.model';

@Injectable()
export default class OrderFormService {
  public readonly form: FormGroup;

  constructor(
    private orderService: OrderService,
    private credentialsService: CredentialsService,
    private basketService: BasketService,
  ) {
    this.form = new FormGroup({
      Address: new FormControl('', [Validators.required]),
    });
  }

  public validateAddress(): string | null {
    if (this.form.controls.Address.valid || this.form.controls.Address.pristine) {
      return null;
    }
    let res: string | null = null;
    if (this.form.controls.Address.errors?.required) {
      res = "Address cann't be empty!";
    }
    return res;
  }

  submit(observer?: Observer<void>) {
    if (!this.form.valid) {
      throw new Error('submit on invalid form');
    } else {
      this.orderService.post(
        new Order(
          this.form.controls.Address.value,
          this.credentialsService.getUserId(),
          this.basketService.goods,
        ),
        {
          next: () => {
            observer?.next();
            this.basketService.clearBasket();
          },
          complete: () => {
            this.basketService.clearBasket();
            observer?.complete();
          },
          error: (err) => observer?.error(err),
        } as Observer<void>,
      );
    }
  }
}
