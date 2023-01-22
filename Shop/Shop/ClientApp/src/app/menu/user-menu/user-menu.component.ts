import { Component, OnInit, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { Subscription } from 'rxjs';
import GoodService from '../../core/services/good.service';
import Good from '../../core/models/good.model';
import Paginator from '../../shared/paginator';
import SearchLineService from '../shared/search-line.service';
import Alert from '../../core/alert';

@Component({
  selector: 'user-menu-component',
  templateUrl: './user-menu.component.html',
  styleUrls: ['./user-menu.component.css'],
})
export default class UserMenuComponent extends Paginator<Good> implements OnInit, OnDestroy {
  private goodsSub: Subscription;

  private searchLineIsEmptySub: Subscription;

  public goods: Good[] | null;

  protected get items(): Good[] | null {
    return this.goods;
  }

  @ViewChild('alertDiv', { static: false })
  alertDiv: ElementRef | undefined;

  constructor(private goodService: GoodService, private searchLineService: SearchLineService) {
    super();
  }

  ngOnInit(): void {
    this.goodsSub = this.goodService.dataGoods$.subscribe((data: Good[] | null) => {
      this.goods = data;
      this.toFirstPage();
      this.checkCollection();
    });
    this.goodService.refreshGoods();

    this.searchLineIsEmptySub = this.searchLineService.searchLineIsEmpty$.subscribe(() =>
      this.goodService.refreshGoods(),
    );
  }

  ngOnDestroy(): void {
    this.goodsSub.unsubscribe();
    this.searchLineIsEmptySub.unsubscribe();
  }

  log(good: Good) {
    console.log(good);
  }

  private checkCollection() {
    if (this.goods === null || this.goods.length === 0) {
      Alert.alertMessage(this.alertDiv, 'No goods found');
    } else {
      Alert.hideAlertMessage(this.alertDiv);
    }
  }
}
