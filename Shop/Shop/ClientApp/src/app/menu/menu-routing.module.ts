import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import MenuComponent from './menu.component';
import UserMenuNavComponent from './user-menu-nav/user-menu-nav.component';
import UserMenuComponent from './user-menu/user-menu.component';
import GoodComponent from './good/good.component';
import BasketComponent from './basket/basket.component';
import OrdersMenuComponent from './orders-menu/orders-menu.component';

import BasicNavComponent from '../shared/basic-nav/basic-nav.component';
import NotFoundComponent from '../shared/not-found.component';

const routes: Routes = [
  { path: '', redirectTo: '/menus/menu/(usermenu//nav:usermenunav)', pathMatch: 'full' },
  {
    path: 'menu',
    component: MenuComponent,
    children: [
      { path: 'usermenu', component: UserMenuComponent, outlet: 'primary' },
      { path: 'usermenunav', component: UserMenuNavComponent, outlet: 'nav' },
      { path: 'basicnav', component: BasicNavComponent, outlet: 'nav' },
      { path: 'ordersmenu', component: OrdersMenuComponent, outlet: 'primary' },
      { path: 'good/:id', component: GoodComponent },
      { path: 'basket', component: BasketComponent },
      { path: '**', component: NotFoundComponent },
    ],
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export default class MenuRoutingModule {}
