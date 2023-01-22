import { Component } from '@angular/core';
import GoodService from '../../core/services/good.service';
import UserService from '../../core/services/user.service';
import SearchLineService from '../shared/search-line.service';

@Component({
  selector: 'user-menu-nav-component',
  templateUrl: './user-menu-nav.component.html',
})
export default class UserMenuNavComponent {
  public searchLine: string | null;

  constructor(
    private searchLineService: SearchLineService,
    private userService: UserService,
    private goodService: GoodService,
  ) {}

  LogOut() {
    this.userService.logOut();
  }

  searchGoods() {
    if (this.searchLine !== '' && this.searchLine !== undefined) {
      this.goodService.searchGoodsByName(this.searchLine.trim());
    } else {
      console.log('searching line is empty');
    }
  }

  checkEmptiness() {
    if (this.searchLine === '') {
      this.searchLineService.emitSearchLineIsEmpty();
    }
  }
}
