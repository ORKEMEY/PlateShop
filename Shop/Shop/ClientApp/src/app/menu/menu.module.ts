import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import MenuRoutingModule from './menu-routing.module';
import SharedModule from '../shared/shared.module';

import MenuComponent from './menu.component';
import UserMenuNavComponent from './user-menu-nav/user-menu-nav.component';
import UserMenuComponent from './user-menu/user-menu.component';
import GoodComponent from './good/good.component';
import BasketComponent from './basket/basket.component';
import OrderFormComponent from './order-form/order-form.component';
import OrdersMenuComponent from './orders-menu/orders-menu.component';

import OrderFormService from './shared/order-form.service';
import SearchLineService from './shared/search-line.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    MenuRoutingModule,
    SharedModule,
  ],
  declarations: [
    MenuComponent,
    UserMenuNavComponent,
    UserMenuComponent,
    GoodComponent,
    BasketComponent,
    OrderFormComponent,
    OrdersMenuComponent,
  ],
  bootstrap: [],
  providers: [OrderFormService, SearchLineService],
  exports: [
    MenuComponent,
    UserMenuNavComponent,
    UserMenuComponent,
    GoodComponent,
    BasketComponent,
    OrderFormComponent,
    OrdersMenuComponent,
  ],
})
export default class MenuModule {}
