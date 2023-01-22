import {
  Component,
  ViewChild,
  ElementRef,
  OnInit,
  EventEmitter,
  Output,
  Input,
} from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Observer } from 'rxjs';
import OrderFormService from '../shared/order-form.service';
// import AddItemService from '../shared/add-item.service';
import Alert from '../../core/alert';

@Component({
  selector: 'order-form',
  templateUrl: 'order-form.component.html',
  styleUrls: ['order-form.component.css'],
})
export default class OrderFormComponent implements OnInit {
  @Input()
  isVisible: boolean = false;

  @Input()
  cost: Number = 0;

  @ViewChild('alertAddressDiv', { static: false })
  alertAddressDiv: ElementRef | undefined;

  @ViewChild('alertCommonDiv', { static: false })
  alertCommonDiv: ElementRef | undefined;

  @Output()
  submitted = new EventEmitter<void>();

  @Output()
  closed = new EventEmitter<void>();

  public get form(): FormGroup {
    return this.orderFormService.form;
  }

  constructor(private orderFormService: OrderFormService) {} // private addItemService: AddItemService, // private questionsAddFormService: QuestionsAddFormService,

  ngOnInit(): void {
    this.form.valueChanges.subscribe(() => Alert.hideAlertMessage(this.alertCommonDiv));
  }

  addressChange() {
    if (this.form.controls.Address.valid) {
      Alert.hideAlertMessage(this.alertAddressDiv);
    }

    const res = this.orderFormService.validateAddress();

    if (res === null) {
      Alert.hideAlertMessage(this.alertAddressDiv);
    } else {
      Alert.alertMessage(this.alertAddressDiv, res);
    }
  }

  submit() {
    try {
      this.orderFormService.submit({
        error: (errMsg: string) => Alert.alertMessage(this.alertCommonDiv, errMsg),
        complete: () => {
          this.close();
          this.submitted.emit();
        },
      } as Observer<void>);
    } catch (error) {
      console.error(error);
    }
  }

  close() {
    this.isVisible = false;
    this.closed.emit();
    this.form.reset();
  }
}
